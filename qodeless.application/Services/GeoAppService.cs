using AutoMapper;
using NLog;
using qodeless.application.Managers.Google;
using qodeless.application.Managers.ViaCep;
using qodeless.application.ViewModels;
using qodeless.domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace qodeless.application
{
    public class GeoAppService : IGeoAppService
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        private ViaCep _viaCep { get; set; } = new ViaCep();
        private GoogleMaps _googleMaps { get; set; } = new GoogleMaps();
        public GeoAppService()
        {
        }
        public void Dispose() => GC.SuppressFinalize(this);
        #region VIACEP
        public AddressViewModel GetFullAddressByZipCode(string zipCode)
        {
            return _viaCep.GetAddressByZipCode(zipCode);
        }
        public List<AddressViewModel> GetZipCodeByAddress(string state, string city, string address)
        {
            return _viaCep.GetZipCodeByAddress(state, city, address);
        }
        #endregion
        #region GOOGLE MAPS API
        public GoogleMapsApiViewModel GetByFullAddress(int number,string district, string state, string city, string address)
        {
            return _googleMaps.GetByFullAddress(number, district, state, city, address);
        }
        public GoogleMapsApiViewModel GetAddressByLatLng(decimal lat,decimal lng)
        {
            return _googleMaps.GetAddressByLatLng(lat, lng);
        }
        public LatLngViewModel GetLatLngByAddress(int number, string district, string state, string city, string address)
        {
            var result = _googleMaps.GetByFullAddress(number, district, state, city, address);
            return new LatLngViewModel((decimal)result.Results.First().Geometry.Location.Lat, (decimal)result.Results.First().Geometry.Location.Lng);
        }
        #endregion
    }
}
