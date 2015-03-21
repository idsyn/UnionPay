using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using com.unionpay.upop.sdk;

namespace UnionPay
{
    public class Submit
    {
        /// <summary>
        /// 实例化,默认UinonPay.config
        /// </summary>
        public Submit()
        {
            UPOPSrv.LoadConf(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UinonPay.config"));
        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="configPath">配置文件全路径</param>
        public Submit(string configPath)
        {
            UPOPSrv.LoadConf(configPath);
        }
        
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="partner">partnetid</param>
        /// <param name="chairtyName">账户名称</param>
        public Submit(string key, string partner, string chairtyName)
        {
            var stream = new Config().SetConfig(key, partner, chairtyName);
            UPOPSrv.LoadConf(stream);
        }

        public void Pay(string title, string orderNo, decimal tradeMoney, string notifyUrl, string returnUrl)
        {
            var param = new Dictionary<string, string>
                {
                    {"transType", UPOPSrv.TransType.CONSUME}, //交易类型，前台只支持CONSUME 和PRE_AUTH
                    {"commodityUrl", ""}, //商品URL
                    {"commodityName", title}, //商品名称
                    {"commodityUnitPrice", "1"}, //商品单价，分为单位
                    {"commodityQuantity", "1"}, //商品数量
                    {"orderNumber", orderNo}, //订单号
                    {"orderAmount", (tradeMoney * 100).ToString().Split('.')[0]}, //交易金额
                    {"orderCurrency", UPOPSrv.CURRENCY_CNY}, //币种
                    {"orderTime", DateTime.Now.ToString("yyyyMMddHHmmss")}, //交易时间
                    {"customerIp", HttpContext.Current.Request.UserHostAddress}, //用户IP
                    {"frontEndUrl", notifyUrl}, //前台回调URL
                    {"backEndUrl", returnUrl} //后台回调URL（前台请求时可为空）
                };
            var srv = new FrontPaySrv(param);
            HttpContext.Current.Response.Write(srv.CreateHtml());
        }
    }
}
