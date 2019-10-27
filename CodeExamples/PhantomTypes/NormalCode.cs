using System;

public class FormData {
    public string Data {
        private set;
        get;
    }

    public bool IsValid {
        private set;
        get;
    }

    public FormData(string data) {
        this.Data = data;
    }

    public void ValidateFormData() {
        this.IsValid = this.Data.Length < 12;
    }
}

public class Database {
    // I sure hope you ALWAYS pass in valid formData
    public void StoreData(FormData formData) {
        Console.WriteLine("Stored: " + formData.Data);
    }

    // I sure like checking that form data is valid EVERY single time this method is called
    public void StoreData2(FormData formData) {
        if (formData.IsValid) {
            Console.WriteLine("Stored: " + formData.Data);
        } else {
            throw new Exception("Oh no, bad form data!!!");
        }
    }
}

class MainClass {
    public static void Main (string[] args) {
        FormData formData = new FormData("hi there");
        new Database().StoreData(formData);
    }
}