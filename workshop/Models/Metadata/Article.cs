using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace workshop.Models
{
    [MetadataType(typeof(ArticleMetadata))]
    public partial class Article
    {
        public class ArticleMetadata
        {
            //告知 mvc 這個欄位來放 Html
            [DataType(DataType.Html)]
            public object ContentText { get; set; }

            [UIHint("SystemUser")]
            public object CreateUser { get; set; }
        }

    }
}