public class JobListing
{
    public string? Url { get; set; }
    public string? Title { get; set; }
    public string? Company { get; set; }
    public string? ID { get; set; }
    public string? Location { get; set; }


    public JobListing(string id, string title, string company, string url, string location)
    {
        ID = id;
        Title = title;
        Company = company;
        Url = url;
        Location = location;
    }

    public string DisplayAttributes()
    {
        return String.Format("{0} - {1,-80} - {2,-40} - {3,-20} - {4}", this.ID, this.Title, this.Company, this.Location, this.Url);
    }

}