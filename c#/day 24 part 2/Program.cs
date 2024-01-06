//the equations I could not derive myself are (V1 - V2)x(P1 - P2) * P = (V0 - V1) * P0xP1
//this one got me completely clueless, I have to get my linear algebra up

List<Hail> hails = [];

using(StreamReader sr = new StreamReader("input.txt"))
{
    string line;
    while((line = sr.ReadLine()) != null)
    {
        hails.Add(new Hail(line));
    }
}

//here i am making a matrix equation C*P = D where D is a vector made of left sides of 3 quations above, P is a vector that we are looking for 
//and C is a vector made of left sides of the above equation
double Dx = Vector.DotProduct(hails[0].v - hails[1].v, Vector.CrossProduct(hails[0].p, hails[1].p));
double Dy = Vector.DotProduct(hails[0].v - hails[2].v, Vector.CrossProduct(hails[0].p, hails[2].p));
double Dz = Vector.DotProduct(hails[1].v - hails[2].v, Vector.CrossProduct(hails[1].p, hails[2].p));
Vector D = new Vector(Dx, Dy, Dz);

Vector c0 = Vector.CrossProduct(hails[0].v - hails[1].v, hails[0].p - hails[1].p);
Vector c1 = Vector.CrossProduct(hails[0].v - hails[2].v, hails[0].p - hails[2].p);
Vector c2 = Vector.CrossProduct(hails[1].v - hails[2].v, hails[1].p - hails[2].p);

Matrix C = new Matrix(c0, c1, c2);

//now we need to find inverse of C
double[,] identityNums = 
{
    {1, 0, 0},
    {0, 1, 0},
    {0, 0, 1}
};

Matrix I = new Matrix(identityNums);

C.Augment = I;

C.Gauss();

Matrix CInverse = C.Augment;

Vector P = CInverse.TimesVector(D);
//for some reason this is one less than the actual result XD but somehow i misscounted or something because i rounded it 2 up 
Console.WriteLine(P.x + P.y + P.z);