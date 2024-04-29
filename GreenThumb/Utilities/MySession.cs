using GreenThumb.Models.DomainModels;

namespace GreenThumb.Utilities
{
	public class MySession
	{
		//private const string PurchaseKey = "purchase";

		//private IRequestCookieCollection requestCookies { get; set; } = null!;
		//private IResponseCookies responseCookies { get; set; } = null!;

		private ISession session { get; set; }
		public MySession(ISession sess) => session = sess;

		public string GetTechnicianId() => session.GetString("TechnicianId") ?? string.Empty;

		public void SetTechnicianId(string technicianId) => session.SetString("TechnicianId", technicianId);

		//public Purchase GetPurchase() => session.GetObject<Purchase>(PurchaseKey) ?? new Purchase();

		//public void SetPurchase(Purchase purchase) => session.SetObject(PurchaseKey, purchase);
	}
}
