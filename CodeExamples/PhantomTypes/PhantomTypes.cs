using System;

public interface ValidationState {}
public class Validated : ValidationState {}
public class Unvalidated : ValidationState {}

public class FormData<A> where A: ValidationState {
    public string Data {
        private set;
        get;
    }

    private FormData(string data) {
        this.Data = data;
    }

    static public FormData<Unvalidated> CreateFormData(string data) {
        return new FormData<Unvalidated>(data);
    }

    static public FormData<Validated> ValidateFormData(FormData<Unvalidated> formData) {
        if (formData.Data.Length < 12) {
            return new FormData<Validated>(formData.Data);
        } else {
            throw new Exception("You just gave bad form data!!!");
        }
    }
}

public class Database {
    // try to pass bad form data here
    public void StoreData(FormData<Validated> formData) {
        Console.WriteLine("Stored: " + formData.Data);
    }
}

class MainClass {
    public static void Main (string[] args) {
        FormData<Unvalidated> formData = FormData<Unvalidated>.CreateFormData("hi there");
        FormData<Validated> validFormData = FormData<Unvalidated>.ValidateFormData(formData);
        new Database().StoreData(validFormData);
    }
}