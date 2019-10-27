using System;

public interface ResponseState {}
public class Start : ResponseState {}
public class Headers : ResponseState {
  public int statusCode;

  public Headers(int statusCode) {
    this.statusCode = statusCode;
  }
}

public class Finish : ResponseState {
  private int statusCode;

  public Finish(int statusCode) {
    this.statusCode = statusCode;
  }
}

public class HttpResponse<T> where T: ResponseState {
  private string state;
  private T extra;

  private HttpResponse() {}

  public static HttpResponse<Start> Create() {
    return new HttpResponse<Start>();
  }

  private HttpResponse(string state, T extra) {
    this.state = state;
    this.extra = extra;
  }

  public static HttpResponse<Headers> StatusLine(HttpResponse<Start> response, int statusCode, string reasonPhrase) {
      return new HttpResponse<Headers>("HTTP/1.1 {0} " + reasonPhrase + "\r\n", new Headers(statusCode));
  }

  public static void Header(HttpResponse<Headers> response, string name, string value) {
      response.state += $"{name}: {value}\r\n";
  }

  public static HttpResponse<Finish> Body(HttpResponse<Headers> response, string body) {
      response.state += $"\r\n{body}";
      return new HttpResponse<Finish>(response.state, new Finish(response.extra.statusCode));
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