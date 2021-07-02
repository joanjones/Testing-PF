using System;
namespace NewPF
{
    public class Shark
    {
        Random random_num = new Random();
        // SETS TYPE OF MEMBER VARIABLE
        public double X;
        public double Y;
        public double Z;
        public double THETA;
        public double V;
        public Shark()
        {
            X = 30;
            Y = 60;
            Z = 3.0;
            THETA  = Math.PI/2;
            V = 3.0;
            
        }

        void create_shark()
        {
            // creates sharks
        }
        void update_shark()
        {
            // should update the sharks position after 
        }
       
    }
}