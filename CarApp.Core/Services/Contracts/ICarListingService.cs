﻿using CarApp.Core.Enumerations;
using CarApp.Core.ViewModels;
using CarApp.Core.ViewModels.CarListing;

namespace CarApp.Core.Services.Contracts
{
    public interface ICarListingService
    {
        Task<CarListingQueryServiceModel> GetAllCarListingsAsync(
            int brandId = 0,
            int modelId = 0,
            int price = 0,
            CarListingSorting sorting = CarListingSorting.BrandModelYear,
            int currentPage = 1,
            int listingsPerPage = 1);

        Task AddCarListingAsync(CarViewModel model, string? user);

        Task<CarDetailsViewModel?> CarListingDetails(int listingId); 
    }
}
