using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qodeless.application
{
    public interface IOrderServices : IDisposable
    {
        IEnumerable<Order> GetAll();
        Order GetById(Guid id);
        bool Add(OrderViewModel vm);
        bool Update(OrderViewModel vm);
        bool Remove(Guid id);
        //IEnumerable<Order> GetAllOrderNoRegistry();
        public List<ResponsePixViewModel> GetVariousPixCode(float value, int quantity, string clientId, string clientSecret, string userId, Guid? siteId);
        public ResponsePixViewModel GetPixCode(long fee, float value, string clientId, string clientSecret, string userId, Guid? siteId, Guid? accountId);
        public ResponseAuthViewModel GetPixAuth(string clientId, string clientSecret);
        public ResponsePixViewModel GetPixStatus(string clientId, string clientSecret, string pixId);
        public bool CheckPixIsPaid(string clientId, string clientSecret, string pixId);
        public OrderViewModel GetByPixId(string pixId);
        public string GenerateQrCode(string clientId, string clientSecret, string pixId);
        Task<KeyValuePair<string, int>> GenerateRandomNumber();
        IEnumerable<OrderViewModel> GetAllBy(Func<Order, bool> exp);
    }
}
