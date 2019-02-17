using System.Collections.Generic;

namespace Prj.Services.Models
{

    public class ProductAdvanceSearchServiceModel
    {
        public long Id { get; set; }

        /// <summary>
        /// نام محصول
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// تصویر محصول
        /// </summary>
        public string Picture { get; set; }

        ///// <summary>
        ///// هزینه ها
        ///// </summary>
        //public ProductCostCalculationServiceModel CostCalculation { get; set; }

        /// <summary>
        /// میزان بازدید
        /// </summary>
        public int Visit { get; set; }


        /// <summary>
        /// آیکون محصول
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// وضعیت فروش آنلاین
        /// </summary>
        public bool ActiveForSale { get; set; }
    }

    public class ProductAdvanceSearchRequirementServiceModel
    {
        public ProductPriceSearchServiceModel Price { get; set; }
        public List<ProductBrandSearchServiceModel> Brands { get; set; }
        public List<ProductColorSearchServiceModel> Colors { get; set; }
        public List<ProductPropertyCategorySearchServiceModel> Properties { get; set; }
        public List<ProductCommonFeatureSearchServiceModel> CommonFeatures { get; set; }
        public List<ProductSizingTypeValueSearchServiceModel> Sizes { get; set; }


        public class ProductBrandSearchServiceModel
        {
            public int BrandId { get; set; }
            public string FaName { get; set; }
            public string EnName { get; set; }
        }

        public class ProductColorSearchServiceModel
        {
            public int ColorId { get; set; }
            public string ColorName { get; set; }
            public string ColorCode { get; set; }
        }
        public class ProductCommonFeatureSearchServiceModel
        {
            public int ProductCommonFeatureId { get; set; }
            public string ProductCommonFeatureName { get; set; }
        }

        public class ProductPropertyCategorySearchServiceModel
        {
            public int ProductPropertyCategoryId { get; set; }
            public string ProductPropertyCategoryName { get; set; }
        }

        public class ProductSizingTypeValueSearchServiceModel
        {
            public int ProductSizingTypeValueId { get; set; }
            public string ProductSizingTypeValueName { get; set; }
        }

        public class ProductPriceSearchServiceModel
        {
            public int MinPrice { get; set; }
            public int MaxPrice { get; set; }
        }
    }


    public class ProductCommonFeatureValueSearchServiceModel
    {
        public long ProductCommonFeatureValueId { get; set; }
        public string ProductCommonFeatureValueName { get; set; }
    }

    public class ProductPropertySearchServiceModel
    {
        public long ProductPropertyId { get; set; }
        public string ProductPropertyName { get; set; }

    }


}

