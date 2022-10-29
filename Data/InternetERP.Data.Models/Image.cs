#nullable disable

namespace InternetERP.Data.Models
{
    using System;

    using InternetERP.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Path { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public string Extension { get; set; }
    }
}
