# UnionPay

对银联支付的二次封装，支持pc端


pay(pc):
    var title = "银联支付";
    var orderNo = ""; //订单号
    var tradeMoney = 0.00;
    var notifyUrl = "host/NotifyCallback.aspx";
    var returnUrl = "host/ReturnUrl.aspx";
    var key = ""; //账户的key
    var partner = ""; //账户的partnerid
    var chairtyName = ""; //账户显示名称
    var unionPay = new UnionPay.Submit(key, partner, chairtyName));
    unionPay.Pay(title, orderNo, tradeMoney, notifyUrl, returnUrl);

return(pc):
    public partial class NotifyUrl : UnionPay.NotifyPage
    {
        protected override void OnNotifyConfirm()
        {
            //todo:业务逻辑处理
        }
    }