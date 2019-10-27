using System;

public interface ResponseState {}
public class Start : ResponseState {}
public class Headers : ResponseState {}
public class Finish : ResponseState {}

public class HttpResponse<T> where T: ResponseState {
  private string state;

  private HttpResponse() {}

  public static HttpResponse<Start> Create() {
    return new HttpResponse<Start>();
  }

  private HttpResponse(string state) {
    this.state = state;
  }

  public static HttpResponse<Headers> StatusLine(HttpResponse<Start> response, int statusCode, string reasonPhrase) {
      return new HttpResponse<Headers>($"HTTP/1.1 {statusCode.ToString()} {reasonPhrase}\r\n");
  }

  public static void Header(HttpResponse<Headers> response, string name, string value) {
      response.state += $"{name}: {value}\r\n";
  }

  public static HttpResponse<Finish> Body(HttpResponse<Headers> response, string body) {
      response.state += $"\r\n{body}";
      return new HttpResponse<Finish>(response.state);
  }
}

class MainClass {
  public static void Main (string[] args) {
    HttpResponse<Start> response = HttpResponse<Start>.Create();
    HttpResponse<Headers> withStatus = HttpResponse<Headers>.StatusLine(response, 200, "OK");
    HttpResponse<Headers>.Header(withStatus, "X-Unexpected", "Spanish-Inquisition");
    HttpResponse<Headers>.Header(withStatus, "Content-Length", "6");
    HttpResponse<Finish> finalResponse = HttpResponse<Finish>.Body(withStatus, "Hello!");
  }
}