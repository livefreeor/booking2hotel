using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HotelReview
/// </summary>
/// 
namespace Hotels2thailand.Reviews
{
    public class ProductReviewsShort
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string Title { get; set; }
        public byte CatId { get; set; }
        public string FullName { get; set; }
        public string ReviewForm { get; set; }
        public byte RateOverAll { get; set; }
        public DateTime DateSubmit { get; set; }




        public ProductReviewsShort()
        {
            //
            // TODO: Add constructor logic here
            //
        }



    }
    public class ProductReviews 
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string FileName { get; set; }
        public string ReivewsFileName { get; set; }
        public string FolderDestination { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<byte> CountryId { get; set; }
        public string Countrytitle { get; set; }
        public byte ReccomId { get; set; }
        public string ReccomTitle { get; set; }
        public byte FromId { get; set; }
        public string FromTitle { get; set; }
        public string Title { get; set; }
        public byte CatId { get; set; }
        public string Detail { get; set; }
        public string Positive { get; set; }
        public string Nagative { get; set; }
        public string FullName { get; set; }
        public string ReviewForm { get; set; }
        public byte RateOverAll { get; set; }
        public byte RateService { get; set; }
        public byte RateLocation { get; set; }
        public byte RateRoom { get; set; }
        public byte RateClean { get; set; }
        public byte RateMoney { get; set; }

        public byte RateFairway { get; set; }
        public byte Rategreen { get; set; }
        public byte RateDifficult { get; set; }
        public byte RateSpeed { get; set; }
        public byte RateCaddy { get; set; }
        public byte RateClubhouse { get; set; }
        public byte RateFood { get; set; }
        public byte RatePerformance { get; set; }
        public byte RatePunctuality { get; set; }
        public byte RateDiagnose_ability { get; set; }
        public byte RatePronunciation { get; set; }
        public byte RateKnowledge { get; set; }

        public int VoteAll { get; set; }
        public int VoteUseFull { get; set; }
        public Nullable<DateTime> DateTravel { get; set; }
        public DateTime DateSubmit { get; set; }
        public bool Status { get; set; }
        public bool StatusBin { get; set; }
        public byte ProductCat { get; set; }

        public ProductReviews()
        {
            //
            // TODO: Add constructor logic here
            //
        }



    }
}