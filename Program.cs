using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Azure;
using Azure.AI.FormRecognizer;
using Azure.AI.FormRecognizer.Models;
using Azure.AI.FormRecognizer.Training;

namespace form_recognizer_1
{
    class Program
    {
        private static readonly string endpoint = "https://formrecog-bkk.cognitiveservices.azure.com/";
        private static readonly string apiKey = "56a89faf9d77462f9b85fb916340d622";
        private static readonly AzureKeyCredential credential = new AzureKeyCredential(apiKey);

        private static readonly string businessCardUrl = "https://raw.githubusercontent.com/Azure/azure-sdk-for-python/master/sdk/formrecognizer/azure-ai-formrecognizer/samples/sample_forms/business_cards/business-card-english.jpg";

        private static FormRecognizerClient AuthenticateClient()
        {
            var client= new FormRecognizerClient(new Uri(endpoint),credential);
            return client;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var businessCardAnalyzer = AnalyzeBusinessCard(client: AuthenticateClient(), assetUrl: businessCardUrl);
            Task.WaitAll(businessCardAnalyzer);
        }

        private static async Task AnalyzeBusinessCard(FormRecognizerClient client, string assetUrl)
        {
            RecognizedFormCollection businessCards = await client.StartRecognizeBusinessCardsFromUriAsync(new Uri(assetUrl)).WaitForCompletionAsync();

            foreach (RecognizedForm businessCard in businessCards)
            {
                FormField ContactNamesField;
                if (businessCard.Fields.TryGetValue("ContactNames", out ContactNamesField))
                {
                    if (ContactNamesField.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField contactNameField in ContactNamesField.Value.AsList())
                        {
                            Console.WriteLine($"Contact Name: {contactNameField.ValueData.Text}");

                            if (contactNameField.Value.ValueType == FieldValueType.Dictionary)
                            {
                                IReadOnlyDictionary<string,
                                FormField> contactNameFields = contactNameField.Value.AsDictionary();

                                FormField firstNameField;
                                if (contactNameFields.TryGetValue("FirstName", out firstNameField))
                                {
                                    if (firstNameField.Value.ValueType == FieldValueType.String)
                                    {
                                        string firstName = firstNameField.Value.AsString();

                                        Console.WriteLine($"    First Name: '{firstName}', with confidence {firstNameField.Confidence}");
                                    }
                                }

                                FormField lastNameField;
                                if (contactNameFields.TryGetValue("LastName", out lastNameField))
                                {
                                    if (lastNameField.Value.ValueType == FieldValueType.String)
                                    {
                                        string lastName = lastNameField.Value.AsString();

                                        Console.WriteLine($"    Last Name: '{lastName}', with confidence {lastNameField.Confidence}");
                                    }
                                }
                            }
                        }
                    }
                }

                FormField jobTitlesFields;
                if (businessCard.Fields.TryGetValue("JobTitles", out jobTitlesFields))
                {
                    if (jobTitlesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField jobTitleField in jobTitlesFields.Value.AsList())
                        {
                            if (jobTitleField.Value.ValueType == FieldValueType.String)
                            {
                                string jobTitle = jobTitleField.Value.AsString();

                                Console.WriteLine($"  Job Title: '{jobTitle}', with confidence {jobTitleField.Confidence}");
                            }
                        }
                    }
                }

                FormField departmentFields;
                if (businessCard.Fields.TryGetValue("Departments", out departmentFields))
                {
                    if (departmentFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField departmentField in departmentFields.Value.AsList())
                        {
                            if (departmentField.Value.ValueType == FieldValueType.String)
                            {
                                string department = departmentField.Value.AsString();

                                Console.WriteLine($"  Department: '{department}', with confidence {departmentField.Confidence}");
                            }
                        }
                    }
                }

                FormField emailFields;
                if (businessCard.Fields.TryGetValue("Emails", out emailFields))
                {
                    if (emailFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField emailField in emailFields.Value.AsList())
                        {
                            if (emailField.Value.ValueType == FieldValueType.String)
                            {
                                string email = emailField.Value.AsString();

                                Console.WriteLine($"  Email: '{email}', with confidence {emailField.Confidence}");
                            }
                        }
                    }
                }

                FormField websiteFields;
                if (businessCard.Fields.TryGetValue("Websites", out websiteFields))
                {
                    if (websiteFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField websiteField in websiteFields.Value.AsList())
                        {
                            if (websiteField.Value.ValueType == FieldValueType.String)
                            {
                                string website = websiteField.Value.AsString();

                                Console.WriteLine($"  Website: '{website}', with confidence {websiteField.Confidence}");
                            }
                        }
                    }
                }

                FormField mobilePhonesFields;
                if (businessCard.Fields.TryGetValue("MobilePhones", out mobilePhonesFields))
                {
                    if (mobilePhonesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField mobilePhoneField in mobilePhonesFields.Value.AsList())
                        {
                            if (mobilePhoneField.Value.ValueType == FieldValueType.PhoneNumber)
                            {
                                string mobilePhone = mobilePhoneField.Value.AsPhoneNumber();

                                Console.WriteLine($"  Mobile phone number: '{mobilePhone}', with confidence {mobilePhoneField.Confidence}");
                            }
                        }
                    }
                }

                FormField otherPhonesFields;
                if (businessCard.Fields.TryGetValue("OtherPhones", out otherPhonesFields))
                {
                    if (otherPhonesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField otherPhoneField in otherPhonesFields.Value.AsList())
                        {
                            if (otherPhoneField.Value.ValueType == FieldValueType.PhoneNumber)
                            {
                                string otherPhone = otherPhoneField.Value.AsPhoneNumber();

                                Console.WriteLine($"  Other phone number: '{otherPhone}', with confidence {otherPhoneField.Confidence}");
                            }
                        }
                    }
                }

                FormField faxesFields;
                if (businessCard.Fields.TryGetValue("Faxes", out faxesFields))
                {
                    if (faxesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField faxField in faxesFields.Value.AsList())
                        {
                            if (faxField.Value.ValueType == FieldValueType.PhoneNumber)
                            {
                                string fax = faxField.Value.AsPhoneNumber();

                                Console.WriteLine($"  Fax phone number: '{fax}', with confidence {faxField.Confidence}");
                            }
                        }
                    }
                }

                FormField addressesFields;
                if (businessCard.Fields.TryGetValue("Addresses", out addressesFields))
                {
                    if (addressesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField addressField in addressesFields.Value.AsList())
                        {
                            if (addressField.Value.ValueType == FieldValueType.String)
                            {
                                string address = addressField.Value.AsString();

                                Console.WriteLine($"  Address: '{address}', with confidence {addressField.Confidence}");
                            }
                        }
                    }
                }

                FormField companyNamesFields;
                if (businessCard.Fields.TryGetValue("CompanyNames", out companyNamesFields))
                {
                    if (companyNamesFields.Value.ValueType == FieldValueType.List)
                    {
                        foreach (FormField companyNameField in companyNamesFields.Value.AsList())
                        {
                            if (companyNameField.Value.ValueType == FieldValueType.String)
                            {
                                string companyName = companyNameField.Value.AsString();

                                Console.WriteLine($"  Company name: '{companyName}', with confidence {companyNameField.Confidence}");
                            }
                        }
                    }
                }
            }
        }
    }
}
