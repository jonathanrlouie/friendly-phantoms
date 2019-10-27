using System;

public class HttpResponse {
  private string response;

  public HttpResponse StatusLine(int statusCode, string reasonPhrase) {
      this.response += $"HTTP/1.1 {statusCode.ToString()} {reasonPhrase}\r\n";
      return this;
  }

  public HttpResponse Header(string name, string value) {
      this.response += $"{name}: {value}\r\n";
      return this;
  }

  public HttpResponse Body(string body) {
      this.response += $"\r\n{body}";
      return this;
  }
}

class MainClass {
  public static void Main (string[] args) {
    HttpResponse response = new HttpResponse().StatusLine(200, "OK")
      .Header("X-Unexpected", "Spanish-Inquisition")
      .Header("Content-Length", "6")
      .Body("Hello!");

    HttpResponse badResponse = new HttpResponse()
      .Header("Content-Length", "6")
      .Body("Hello!");

    HttpResponse badResponse2 = new HttpResponse()
      .Body("Hello!")
      .Header("Content-Length", "6");
  }
}