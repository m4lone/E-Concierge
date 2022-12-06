using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using qodeless.application.ViewModels;
using qodeless.domain.Entities;
using qodeless.domain.Model;

namespace qodeless.application
{
    public interface IGeoAppService : IDisposable
    {
        AddressViewModel GetFullAddressByZipCode(string zipCode);
        List<AddressViewModel> GetZipCodeByAddress(string state, string city, string address);
        GoogleMapsApiViewModel GetByFullAddress(int number, string district, string state, string city, string address);
        GoogleMapsApiViewModel GetAddressByLatLng(decimal lat, decimal lng);
        LatLngViewModel GetLatLngByAddress(int number, string district, string state, string city, string address);
    }
}