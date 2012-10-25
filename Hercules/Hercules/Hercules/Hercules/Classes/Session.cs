using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MME.Hercules
{
    /// <summary>
    /// Represents the current session (tracks selected color type, backgrounds, email etc.)
    /// </summary>
    public class Session
    {
        public Session()
        {
            this.SelectedBackgrounds = new List<int>();
            this.StartDate = DateTime.Now;
            this.ID = Guid.NewGuid();
            this.StartUserDate = DateTime.Now;            
        }

        public string PhotoPath
        {
            get
            {
                return ConfigUtility.StoreImagesPath + this.StartDate.Ticks.ToString();
            }
        }

        public Guid ID
        { get; set; }

        public DateTime BirthDate
        { get; set; }

        public DateTime StartDate
        { get; set; }

        public DateTime StartUserDate
        { get; set; }

        public DateTime EndUserDate
        { get; set; }

        public DateTime EndDate
        { get; set; }

        public string EmailAddress
        { get; set; }

        public List<int> SelectedBackgrounds
        { get; set; }

        public ColorType SelectedColorType
        { get; set; }

        public int FavoritePhoto
        { get; set; }

        public string FavoritePhotoFilename
        { get; set; }


        public string FacebookAccessToken
        { get; set; }


        public string FacebookExpires
        { get; set; }

        public List<string> Responses
        { get; set; }

        public int MaxCopies
        { get; set; }
    }
}
