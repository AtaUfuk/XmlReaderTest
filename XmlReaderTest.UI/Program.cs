using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using XmlReaderTest.Business;
using XmlReaderTest.UI.Models.NumberToDollar;
// See https://aka.ms/new-console-template for more information
Console.Write("Lütfen Bir Sayı Giriniz:");
string dolar = Console.ReadLine() ?? "10";
Console.WriteLine("Sonuç:" + GetResult(dolar));
Console.ReadKey();
string GetResult(string input)
{
    NumberToDollars request = new NumberToDollars();
    request.dNum = decimal.Parse(input);
    Envelope envelope = new Envelope();
    Body body = new Body();
    body.NumberToDollars = request;
    envelope.Body = body;
    XmlHelper xmlHelper = new XmlHelper();
    Dictionary<string, string> data = new Dictionary<string, string>();
    data.Add("Accept", "text/xml; charset=utf-8");
    var result = xmlHelper.GetXmlResponse<Envelope, ResponseEnvelope>(envelope, "https://www.dataaccess.com/webservicesserver/NumberConversion.wso?op=NumberToDollars", HttpMethod.Post, System.Text.Encoding.UTF8, data);

    return result.Body.NumberToDollarsResponse.NumberToDollarsResult;
}

string GetResultProxy(string input)
{
    NumberToDollars request = new NumberToDollars();
    request.dNum = decimal.Parse(input);
    Envelope envelope = new();
    Body body = new()
    {
        NumberToDollars = request
    };
    envelope.Body = body;
    XmlHelper xmlHelper = new();
    Dictionary<string, string> data = new Dictionary<string, string>();
    data.Add("Accept", "text/xml; charset=utf-8");
    WebProxy proxy = new()
    {
        Address = new Uri("https://192.168.1.1:3131")
    };
    HttpClientHandler clientHandler = new()
    {
        Proxy = proxy,
        AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
        AllowAutoRedirect = true,
        ClientCertificateOptions = ClientCertificateOption.Manual,
        SslProtocols = SslProtocols.Tls12
    };
    clientHandler.ClientCertificates.Add(new X509Certificate2("cert.crt"));
    var result = xmlHelper.GetXmlResponseWithProxy<Envelope, ResponseEnvelope>(envelope, "https://www.dataaccess.com/webservicesserver/NumberConversion.wso?op=NumberToDollars", HttpMethod.Post, System.Text.Encoding.UTF8, clientHandler, data);

    return result.Body.NumberToDollarsResponse.NumberToDollarsResult;
}

