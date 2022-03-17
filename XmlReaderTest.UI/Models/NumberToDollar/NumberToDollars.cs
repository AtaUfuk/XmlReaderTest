using System.Xml.Serialization;

namespace XmlReaderTest.UI.Models.NumberToDollar
{
    [XmlRoot(ElementName = "NumberToDollars", Namespace = "http://www.dataaccess.com/webservicesserver/")]
    public class NumberToDollars
    {
        [XmlElement(ElementName = "dNum",Namespace = "http://www.dataaccess.com/webservicesserver/")]
        public decimal dNum { get; set; }
    }

	[XmlRoot(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	public class Body
	{
		[XmlElement(ElementName = "NumberToDollars", Namespace = "http://www.dataaccess.com/webservicesserver/")]
		public NumberToDollars NumberToDollars { get; set; }
	}

	[XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	public class Envelope
	{
		[XmlElement(ElementName = "Header", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		public string Header { get; set; }
		[XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
		public Body Body { get; set; }
		[XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Soap { get; set; }
		[XmlAttribute(AttributeName = "web", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Web { get; set; }
	}
}
