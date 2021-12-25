/* Calculates a sine wave through the Matt Parker Christmas tree */
Console.WriteLine(DateTime.Now);
Console.WriteLine("Starting");

var csv = new List<string>();
var header = "FRAME_ID";
var LEDCount = 0;
var Coordinates = new List<Coord>();
var maxZ = 0.0;

/* Read in the coordinates */
foreach (var coordLine in File.ReadAllLines("coords_2021.csv"))
{
    header += $",R_{LEDCount},G_{LEDCount},B_{LEDCount}";
    var coords = coordLine.Split(",");
    var coord = new Coord()
    {
        ID = LEDCount,
        X = float.Parse(coords[0]),
        Y = float.Parse(coords[1]),
        Z = float.Parse(coords[2])
    };

    if (coord.Z > maxZ)
        maxZ = coord.Z;

    Coordinates.Add(coord);
    LEDCount++;
}

csv.Add(header);
Console.WriteLine($"Max Z: {maxZ}");
Console.WriteLine($"Number of LEDs: {LEDCount}");

/* Parameters of the sine wave */
var Amplitude = 2;
var k = Math.PI / 2;
var period = 2.0;
var t = 0.0;

/* Calculate all the frames */
for (int frameID = 0; t < (period * 4); frameID++)
{
    var frame = $"{frameID}";
    t = (float)(frameID) / 60;

    foreach (var coord in Coordinates)
    {
        var r = Math.Sqrt(Math.Pow(coord.X, 2) + Math.Pow(coord.Y, 2));
        var z = (Amplitude / 2) * Math.Sin(k * r + t * 2 * Math.PI / period) + maxZ / 3;
        var color = 255 - (int)(150 * Math.Abs(z - coord.Z) / ((maxZ / 3 + Amplitude / 2)));

        if (color < 0)
            color = 0;

        if (z < coord.Z)
        {
            frame += $",{color},0,0";
        }
        else
        {
            frame += $",0,0,{color}";
        }
    }
    Console.WriteLine($"Calculated frame: {frameID} time {t}");
    csv.Add(frame);
}

File.WriteAllLines("output.csv", csv);
Console.WriteLine(DateTime.Now);
Console.WriteLine("Done");