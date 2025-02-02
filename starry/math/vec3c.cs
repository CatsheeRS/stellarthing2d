namespace starry;

public class vec3c
{
    public double x { get; set; }
    public double y { get; set; }
    public double z { get; set; }

    public vec3c(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public vec3 asVec3()
    {
        return new vec3(x, y, z);
    }
    
    public void fromVec3(double x, double y, double z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    
    public void fromVec3(vec3 vec)
    {
        x = vec.x;
        y = vec.y;
        z = vec.z;
    }
}