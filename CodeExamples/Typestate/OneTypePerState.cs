using System;

public class HttpResponse {
  public HttpResponseAfterStatus StatusLine(int statusCode, string reasonPhrase) {
      return new HttpResponseAfterStatus($"HTTP/1.1 {statusCode.ToString()} {reasonPhrase}\r\n");
  }
}

// No simple way to expose the type but hide the constructor. Big sad :(
public class HttpResponseAfterStatus {
  private string responseState;

  public HttpResponseAfterStatus(string responseState) {
    this.responseState = responseState;
  }

  public HttpResponseAfterStatus Header(string name, string value) {
      this.responseState += $"{name}: {value}\r\n";
      return this;
  }

  public HttpResponseAfterBody Body(string body) {
      this.responseState += $"\r\n{body}";
      return new HttpResponseAfterBody(this.responseState);
  }
}

public class HttpResponseAfterBody {
  private string responseState;

  public HttpResponseAfterBody(string responseState) {
    this.responseState = responseState;
  }
}

class MainClass {
  public static void Main (string[] args) {
    HttpResponseAfterBody response = new HttpResponse()
      .StatusLine(200, "OK")
      .Header("X-Unexpected", "Spanish-Inquisition")
      .Header("Content-Length", "6")
      .Body("Hello!");

    // Now these won't compile
    /*HttpResponseAfterBody badResponse = new HttpResponse()
      .Header("Content-Length", "6")
      .Body("Hello!");

    HttpResponseAfterBody badResponse2 = new HttpResponse()
      .Body("Hello!")
      .Header("Content-Length", "6");*/
  }
}