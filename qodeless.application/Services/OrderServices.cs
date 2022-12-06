using AutoMapper;
using NLog;
using qodeless.application.Extensions;
using qodeless.application.Managers.Pix;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Interfaces.Repositories;
using qodeless.domain.Validators;
using Qodeless.Domain.Enum;
using SkiaSharp;
using SkiaSharp.QrCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace qodeless.application
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        protected static Logger Logger = LogManager.GetCurrentClassLogger();
        private static Random _rnd = new Random();
        private PixManager _pixManager { get; set; } = new PixManager();

        public OrderServices(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        #region CRUD
        private readonly object generateCodeLock = new object();
        public bool Add(OrderViewModel vm)
        {
            var serieResult = GenerateRandomNumber().Result;
            vm.Code = serieResult.Value.ToString();

            var entity = _mapper.Map<Order>(vm);
            var validationResult = new OrderAddValidator().Validate(entity);
            if (validationResult.IsValid)
            {
                _orderRepository.Add(entity);
                return true;
            }
            return false;

        }

        public void Dispose() => GC.SuppressFinalize(this);

        public IEnumerable<Order> GetAll() => _orderRepository.GetAll();

        //public IEnumerable<Order> GetAllOrderNoRegistry() => _orderRepository.GetAllOrderNoRegistry().OrderByDescending(c => c.Date);
        public Order GetById(Guid id) => _orderRepository.GetById(id);

        public bool Remove(Guid id)
        {
            _orderRepository.ForceDelete(id);
            return true;
        }

        public bool Update(OrderViewModel vm)
        {
            var entity = _mapper.Map<Order>(vm);
            var validationResult = new OrderUpdateValidator().Validate(entity);
            if (validationResult.IsValid)
            {
                _orderRepository.Update(entity);
                return true;
            }
            return false;
        }

        public OrderViewModel GetByPixId(string pixId) => _mapper.Map<OrderViewModel>(_orderRepository.GetAllBy(_ => _.PixId == pixId).FirstOrDefault());
        public IEnumerable<OrderViewModel> GetAllBy(Func<Order, bool> exp)
        {
            return _mapper.Map<IEnumerable<OrderViewModel>>(_orderRepository.GetAllBy(exp));
        }
        #endregion

        #region PIX SERVICES
        public ResponseAuthViewModel GetPixAuth(string clientId, string clientSecret)
        {
            Logger.Info("== Getting Auth Pix via API ==");
            return _pixManager.GetPixAuth(clientId, clientSecret);
        }

        public List<ResponsePixViewModel> GetVariousPixCode(float value,int quantity, string clientId, string clientSecret, string userId, Guid? siteId)
        {
            Logger.Info("== Getting pix via API ==");
            var response = new List<ResponsePixViewModel>();
            for (var i = 0; i < quantity; i++)
            {
                var randNumber = GenerateRandomNumber().Result;
                var authToken = _pixManager.GetPixAuth(clientId, clientSecret);
                var result = _pixManager.GetPix(authToken.AccessToken, value);
                var order = new Order()
                {
                    Value = value,
                    Date = result.ExpirationDate.AddDays(3),
                    EOrderStatus = 0,
                    PixId = result.PixId,
                    QrCode = result.Qrcode,
                    Code = randNumber.Value.ToString(),
                    UserId = userId,
                    SiteId = siteId
                };
                _orderRepository.Add(order);
                result.Code = randNumber.Value.ToString();
                result.Serie = randNumber.Key;
                response.Add(result);
            }
            return response;
        }

        public ResponsePixViewModel GetPixCode(long fee, float value, string clientId, string clientSecret, string userId, Guid? siteId, Guid? accountId)
        {
            Logger.Info("== Getting pix via API ==");
            var randNumber = GenerateRandomNumber().Result;
            var authToken = _pixManager.GetPixAuth(clientId, clientSecret);
            var result = _pixManager.GetPix(authToken.AccessToken, value);
            var order = new Order()
            {
                Value = value,
                Fee = fee,
                Date = result.ExpirationDate,
                EOrderStatus = EOrderStatus.Unpaid,
                PixId = result.PixId,
                QrCode = result.Qrcode,
                Code = randNumber.Value.ToString(),
                UserId = userId,
                SiteId = siteId,
                AccountId = accountId
            };
            var res = _orderRepository.Add(order);
            if (res)
            {
                result.Code = randNumber.Value.ToString();
                result.Serie = randNumber.Key;
                return result;
            }
            return new ResponsePixViewModel();
        }

        public ResponsePixViewModel GetPixStatus(string clientId, string clientSecret, string pixId)
        {
            Logger.Info("== Getting Pix Status via API ==");
            var authToken = _pixManager.GetPixAuth(clientId, clientSecret);
            return _pixManager.GetPixStatus(authToken.AccessToken, pixId);
        }

        public bool CheckPixIsPaid(string clientId, string clientSecret, string pixId) => GetPixStatus(clientId, clientSecret, pixId).Paid;

        #endregion

        public string GenerateQrCode(string clientId, string clientSecret, string pixId)
        {
            var generator = new QRCodeGenerator();
            var lvl = ECCLevel.H;

            var texto = GetPixStatus(clientId, clientSecret, pixId).Qrcode;
            var content = texto;

            var qr = generator.CreateQrCode(content, lvl);
            var inf = new SKImageInfo(256, 256);

            var srfc = SKSurface.Create(inf);
            var cnvs = srfc.Canvas;
            cnvs.Render(qr, 256, 256, SKColors.White, SKColors.Black);

            var img = srfc.Snapshot();
            var dat = img.Encode(SKEncodedImageFormat.Jpeg, 100);
            byte[] buffer = StreamExtensions.ReadToEnd(dat.AsStream());
            var base64Image = Convert.ToBase64String(buffer);

            img.Dispose(); dat.Dispose();

            return base64Image;
        }

        public async Task<KeyValuePair<string,int>> GenerateRandomNumber()
        {
            var serieResult = new KeyValuePair<string, int>();
            const string FIRST_SERIE = "01";
            //Last Serie
            var lastSerie = _orderRepository.GetAll().Select(c => c.Code.Length >= 2 ? c.Code.Substring(0, 2) : c.Code ).Distinct().Max();

            if (string.IsNullOrEmpty(lastSerie))
                lastSerie = FIRST_SERIE;

            //Orders from Current Serie only
            var allOrders = _orderRepository.GetAllBy(c => (c.Code.Length >= 2 ? c.Code.Substring(0, 2) : c.Code) == lastSerie).ToList();

            //RandNumber
            var RandNumber = _rnd.Next(0, 1000000);

            var result = await Task.Run(() =>
            {
                while (true)
                {
                    if (!allOrders.Any(c => c.Code.Split(" ").Length >= 3 ? c.Code.Split(" ")[2] == RandNumber.ToString() : false ))
                    {
                        serieResult = new KeyValuePair<string, int>(lastSerie,RandNumber);
                        return serieResult;
                    }

                    RandNumber = _rnd.Next(0, 1000000);
                }
            });

            return serieResult;
        }

    }
}
