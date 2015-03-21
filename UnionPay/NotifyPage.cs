using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace UnionPay
{
    public abstract class NotifyPage : System.Web.UI.Page
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        protected string OrderNo { get; private set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        protected decimal TradeMoney { get; private set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        protected string TradeStatus { get; private set; }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        protected abstract void OnNotifyConfirm();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            OrderNo = Request.Form["orderNumber"];
            TradeMoney = Convert.ToDecimal(Request.Form["orderAmount"]);
            TradeStatus = Request.Form["respCode"];

            if (TradeStatus == "00")
            {
                OnNotifyConfirm();
                Response.Write("200");
            }
            Response.End();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        protected void WriteLog(string text, string logType)
        {
            var strPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "/log/UnionPay/");
            var dateFloderName = DateTime.Now.ToString("yyyyMM");
            strPath = string.Format("{0}/{1}", strPath, dateFloderName);
            if (!Directory.Exists(strPath)) Directory.CreateDirectory(strPath);
            strPath = strPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + ".txt";
            var fs = new StreamWriter(strPath, true, Encoding.Default);
            fs.Write(text);
            fs.Close();
        }
    }
}
