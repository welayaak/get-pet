using System.Runtime.Serialization;

namespace Pet_Get.Models;

[DataContract]
public class DataPoint
{
    public DataPoint( string label, int y)
    {
        this.label = label;
        this.y = y;
    }
    public DataPoint(int userCount)
    {
        this.userCount = userCount;
        this.y = y;
    }
    
    // public DataPoint(int Posts, DateTime Date)
    // {
    //     this.Date = Date;
    //     this.Posts = Posts;
    // }
 
    [DataMember(Name = "label")]
    public string label { get; set; }

    [DataMember(Name = "userCount")]
    public int userCount { get; set; }
    
    [DataMember(Name = "y")]
    public int y { get; set; }
    
    // //Explicitly setting the name to be used while serializing to JSON.
    // [DataMember(Name = "label")]
    // public DateTime Date = new DateTime();
    //
    // //Explicitly setting the name to be used while serializing to JSON.
    // [DataMember(Name = "sold")]
    // public Nullable<int> Posts = null;

}