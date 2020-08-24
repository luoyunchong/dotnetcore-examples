using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace MiniProgramApi.Model
{
  /// <summary>
  /// 商家
  /// </summary>
  [JsonObject(MemberSerialization.OptIn), Table(Name = "Seller")]
  public partial class Seller
  {
    [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
    public int ID { get; set; }

    [JsonProperty, Column(IsIgnore = true)]
    public string ibnumber { get; set; } = null;

    [JsonProperty]
    [Required(ErrorMessage = "商家店名不能为空")]
    [StringLength(50, ErrorMessage = "商家店名输入过长，不能超过50位")]
    public string name { get; set; }
    [JsonProperty]
    public string description { get; set; }
    [JsonProperty]
    public int? deliveryTime { get; set; }
    [JsonProperty]
    public float? score { get; set; }
    [JsonProperty]
    public float? serviceScore { get; set; }
    [JsonProperty]
    public float? foodScore { get; set; }
    [JsonProperty]
    public float? rankRate { get; set; }
    [JsonProperty]
    public int? minPrice { get; set; }
    [JsonProperty]
    public int? deliveryPrice { get; set; }
    [JsonProperty]
    public int? ratingCount { get; set; }
    [JsonProperty]
    public int? sellCount { get; set; }
    [JsonProperty]
    public string bulletin { get; set; }
    [JsonProperty]
    public string avatar { get; set; } = @"https://api.freepos.es/files/sample/seller_icon1.jpg";
    [JsonProperty]
    public string avatarvip { get; set; } = @"https://api.freepos.es/files/sample/user_icon.jpg";

    [JsonProperty]
    [Required(ErrorMessage = "商家电话不能为空")]
    [StringLength(50, ErrorMessage = "商家电话输入过长，不能超过50位")]
    public string customerService { get; set; }
    [JsonProperty]
    public string onlineService { get; set; }

    [JsonProperty, Navigate("SellerID")]
    public virtual List<SellerSupport> supports { get; set; }
    [JsonProperty, Navigate("SellerID")]
    public virtual List<SellerPics> pics { get; set; }
    [JsonProperty, Navigate("SellerID")]
    public virtual List<SellerInfos> infos { get; set; }
  }

  /// <summary>
  /// 商家活动
  /// </summary>
  [JsonObject(MemberSerialization.OptIn), Table(Name = "SellerSupport")]
  public partial class SellerSupport
  {
    [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
    public int ID { get; set; }
    [JsonProperty]
    public int? SellerID { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    [JsonProperty]
    public int? type { get; set; }
    [JsonProperty]
    public string description { get; set; }
  }

  /// <summary>
  /// 商家图片
  /// </summary>
  [JsonObject(MemberSerialization.OptIn), Table(Name = "SellerPics")]
  public partial class SellerPics
  {
    [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
    public int ID { get; set; }
    [JsonProperty]
    public int? SellerID { get; set; }
    [JsonProperty]
    public string Pic { get; set; }
  }

  /// <summary>
  /// 商家介绍
  /// </summary>
  [JsonObject(MemberSerialization.OptIn), Table(Name = "SellerInfos")]
  public partial class SellerInfos
  {
    [JsonProperty, Column(IsPrimary = true, IsIdentity = true)]
    public int ID { get; set; }
    [JsonProperty]
    public int? SellerID { get; set; }
    [JsonProperty]
    public string Info { get; set; }
  }

}
