using System;
using System.Collections.Generic;
namespace NewPF
{
    class ParticleFilter
    {
        public int NUMBER_OF_PARTICLES;
        public double Current_Time;
        List<Particle> particleList = new List<Particle>();
        public Shark s1;
        public Robot r1;
        public List<double> w1_list_x;
        public List<double> w1_list_y;

        public List<double> w2_list_x;
        public List<double> w2_list_y;

        public List<double> w3_list_x;
        public List<double> w3_list_y;

        public ParticleFilter()

        {
            Current_Time = 0;
            NUMBER_OF_PARTICLES = 15;
            r1 = new Robot();
            s1 = new Shark();
            w1_list_x = new List<double>();
            w2_list_x = new List<double>();
            w3_list_x = new List<double>();
            w1_list_y = new List<double>();
            w2_list_y = new List<double>();
            w3_list_y = new List<double>();
        }

        public double angle_wrap(double ang)
        {
            if (-Math.PI <= ang & ang <= Math.PI)
            {
                return ang;
            }
            else
            {
                ang = ang % Math.PI;
                return angle_wrap(ang);
            }
        }
        public double velocity_wrap(double vel)
        {
            if (vel <= 5)
            {
                return vel;
            }
            else
            {
                vel += -5;
                return velocity_wrap(vel);
            }
        }
        double calc_range_error()
        {
            // calculates the average particles position to the true sharks' position
            //double auvRange = Math.Sqrt(Math.Pow((s1.Y-r1.Y), 2) + Math.Pow((r1.X - s1.X), 2));
            return r1.X;
        }
        double calc_alpha_error()
        {
            // calculates the average particles position to the true sharks' position
            //double auvAlpha = angle_wrap(Math.Atan2((r1.Y - s1.Y), (r1.X - s1.X))) - r1.THETA;
            return 5.0;
        }

        public void create()
        {
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            { 
                particleList.Add(new Particle());
            }
        }
        public void update()
        {
            // updates particles while simulated
            // returns new list of updated particles

            for (int i = 0; i < particleList.Count; ++i)
            {
                particleList[i].updateParticles();
            }

        }
        public void update_weights()
        {
            // normalize new weights for each new shark measurement
            double auv_range = this.calc_range_error();
            //Console.WriteLine(auv_range);
            double auv_alpha = this.calc_alpha_error();
            
            for (int i = 0; i < particleList.Count; ++i)
            {
                double particle_range = particleList[i].calc_particle_range(r1.X, r1.Y);
                double particle_alpha = particleList[i].calc_particle_alpha(r1.X, r1.Y, r1.THETA);
                particleList[i].weight(auv_alpha, particle_alpha, auv_alpha, particle_alpha);
            }
            


        }
        public void correct()
        {
            //corrects the particles, adding more copies of particles based on how high the weight is
            int length_particleList = particleList.Count;

            for (int i = 0; i < length_particleList; ++i)
            {
                if (particleList[i].W <= 0.333)
                {
                    Particle particle1 = particleList[i].DeepCopy();
                    particleList.Add(particle1);
                   

                }
                else if (particleList[i].W <= 0.666)
                {
                    Particle particle1 = particleList[i].DeepCopy();
                    particleList.Add(particle1);
                    Particle particle2 = particleList[i].DeepCopy();
                    particleList.Add(particle2);

                }
                else
                {
                    Particle particle1 = particleList[i].DeepCopy();
                    particleList.Add(particle1);
                    Particle particle2 = particleList[i].DeepCopy();
                    particleList.Add(particle2);
                    Particle particle3 = particleList[i].DeepCopy();
                    particleList.Add(particle3);
                    Particle particle4 = particleList[i].DeepCopy();
                    particleList.Add(particle4);
                }

            }
        }

        public void weight_list_x()
        {
            for (int i = 0; i < particleList.Count; ++i)
            {
                if (particleList[i].W <= 0.333)
                {
                    w1_list_x.Add(particleList[i].X);
                }
                else if (particleList[i].W <= 0.666)
                {
                    w2_list_x.Add(particleList[i].X);
                }
                else
                {
                    w3_list_x.Add(particleList[i].X);
                }

            }

        }
        public void weight_list_y()
        { 
            for (int i = 0; i < particleList.Count; ++i)
            {
                if (particleList[i].W <= 0.333)
                {
                    w1_list_y.Add(particleList[i].Y);
                }
                else if (particleList[i].W <= 0.666)
                {
                    w2_list_y.Add(particleList[i].Y);
                }
                else
                {
                    w3_list_y.Add(particleList[i].Y);
                }
            }
        }

        public static void Main(string[] args)
        {
            ParticleFilter p1 = new ParticleFilter();
            //Console.WriteLine(p1.r1.X);
            
            p1.create();
            
           
            //Console.WriteLine(p1.particleList[0].X);
            //Console.WriteLine(p1.particleList[0].W);

            p1.update();
            //Console.WriteLine(p1.particleList[0].X);


            p1.update_weights();
            //Console.WriteLine(p1.particleList[0].W);
            //Console.WriteLine(p1.particleList.Count);
            
            p1.correct();
            //Console.WriteLine(p1.particleList.Count);

            p1.weight_list_y();
            p1.weight_list_x();
            Console.WriteLine("Number of particles in list");
            Console.WriteLine(p1.particleList.Count);
            Console.WriteLine("counts");

            Console.WriteLine("Number of y in w1");
            Console.WriteLine(p1.w1_list_y.Count);
            Console.WriteLine("Number of x in w1");
            Console.WriteLine(p1.w1_list_x.Count);

            Console.WriteLine("Number of y in w2");
            Console.WriteLine(p1.w2_list_y.Count);
            Console.WriteLine("Number of x in w2");
            Console.WriteLine(p1.w2_list_x.Count);

            Console.WriteLine("Number of y in w3");
            Console.WriteLine(p1.w3_list_y.Count);
            Console.WriteLine("Number of x in w3");
            Console.WriteLine(p1.w3_list_x.Count);
            /*
            Console.WriteLine(p1.w1_list_y[0]);
            Console.WriteLine(p1.w2_list_y[0]);
            Console.WriteLine(p1.w3_list_y[0]);
            
            p1.w1_list_y = p1.weight_list_y(w1);
            p1.w2_list_y = p1.weight_list_y(w2);
            p1.w3_list_y = p1.weight_list_y(w3);
            Console.WriteLine(p1.w2_list_y[0]);
            Console.WriteLine(p1.w3_list_y[0]);
            Console.WriteLine(p1.w1_list_y[0]);



            */

        }
    }
}