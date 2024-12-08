namespace Ejad.Core;

public static class ApiRouts
{
    private const string BaseUrl = "/api";

    public static class Applicants
    {
        private const string ApplicantsBaseUrl = BaseUrl + "/applicants";

        public const string Add = ApplicantsBaseUrl;
        public const string Details = ApplicantsBaseUrl + "/{id}";
        public const string Image = ApplicantsBaseUrl + "/images/{imageName}";
    }

}