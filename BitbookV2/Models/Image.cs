using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitbookV2.Models
{
   

    public class Image
    {
        public int Id { set; get; }

        public byte[] ImageData { set; get; }

        public int PostId { set; get; }
        public Post Post { set; get; }


        public int RegistrationId { set; get; }
        public Registration Registration { set; get; }
   
    }
}