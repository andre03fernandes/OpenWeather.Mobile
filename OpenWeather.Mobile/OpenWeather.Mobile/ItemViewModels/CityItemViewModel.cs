﻿using OpenWeather.Mobile.Models;
using OpenWeather.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace OpenWeather.Mobile.ItemViewModels
{
    public class CityItemViewModel : CityResponse
    {
        private readonly INavigationService _navigationService;
        private DelegateCommand _selectCityCommand;

        public CityItemViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public DelegateCommand SelectCityCommand => _selectCityCommand ?? (_selectCityCommand = new DelegateCommand(SelectCityAsync));
        private async void SelectCityAsync()
        {
            NavigationParameters parameters = new NavigationParameters
            {
                {"city", this}
            };

            await _navigationService.NavigateAsync(nameof(CityDetailPage), parameters);
        }
    }
}
