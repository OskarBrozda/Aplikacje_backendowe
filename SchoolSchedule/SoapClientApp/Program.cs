using ServiceReference1;

IUserService soapServiceChannel = new UserServiceClient(UserServiceClient.EndpointConfiguration.BasicHttpBinding_IUserService);
var registerUserResponse = await soapServiceChannel.RegisterUserAsync(new User()
{
    FirstName = "Oskar",
    LastName = "Brozda",
    EmailAddress = "milegodnia@zycze.ja"
});
Console.WriteLine(registerUserResponse);
Console.ReadKey();
