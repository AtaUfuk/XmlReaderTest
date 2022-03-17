using System.Xml.Serialization;

namespace XmlReaderTest.UI.Models.NumberToDollar
{
	[XmlRoot(ElementName = "NumberToDollarsResponse", Namespace = "http://www.dataaccess.com/webservicesserver/")]
	public class NumberToDollarsResponse
	{
		[XmlElement(ElementName = "NumberToDollarsResult", Namespace = "http://www.dataaccess.com/webservicesserver/")]
		public string NumberToDollarsResult { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

	[XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ResponseBody
	{
		[XmlElement(ElementName = "NumberToDollarsResponse", Namespace = "http://www.dataaccess.com/webservicesserver/")]
		public NumberToDollarsResponse NumberToDollarsResponse { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	public class ResponseEnvelope
	{
		[XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public ResponseBody Body { get; set; }
		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }
	}
}
