﻿using OnlinerNotifier.BLL.Models.TrackingModels;
using OnlinerNotifier.DAL;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class TrackingService : ITrackingService
    {
        private UnitOfWork unitOfWork;

        public TrackingService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool ChangeStatus(TrackingModel trackingModel)
        {
            var userProduct = unitOfWork.UserProducts.Get(trackingModel.Id);
            if (userProduct != null)
            {
                userProduct.IsTracked = trackingModel.Status;
                unitOfWork.Save();
                return true;
            }
            return false;
        }
    }
}