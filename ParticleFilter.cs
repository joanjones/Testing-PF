using System;
using System.Collections.Generic;
namespace NewPF
{
    class ParticleFilter
    {
        public int NUMBER_OF_PARTICLES;
        public double Current_Time;
        List<Particle> particleList = new List<Particle>();
        Shark s1;
        Robot r1;
        private List<double> w1_list_x;
        private List<double> w1_list_y;

        private List<double> w2_list_x;
        private List<double> w2_list_y;

        private List<double> w3_list_x;
        private List<double> w3_list_y;

        public ParticleFilter()

        {
            Current_Time = 0;
            NUMBER_OF_PARTICLES = 15;
            Shark s1 = new Shark();
            Robot r1 = new Robot();
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
            double auvRange = Math.Sqrt(Math.Pow((r1.Y - s1.Y), 2) + Math.Pow((r1.X - s1.X), 2));
            return auvRange;
        }
        double calc_alpha_error()
        {
            // calculates the average particles position to the true sharks' position
            double auvAlpha = angle_wrap(Math.Atan2((r1.Y - s1.Y), (r1.X - s1.X))) - r1.THETA;
            return auvAlpha;
        }

        public void create()
        {
            for (int i = 0; i < NUMBER_OF_PARTICLES; ++i)
            {
                //Particle particlei = new Particle();
                //particleList.Add(particlei);
                particleList[i] = new Particle();
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
            for (int i = 0; i < particleList.Count; ++i)
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

        public List<double> weight_list_x(double weight_number)
        {
            List<double> weight_list_x = new List<double>();
            for (int i = 0; i < particleList.Count; ++i)
            {
                if (particleList[i].W <= weight_number)
                {
                    weight_list_x.Add(particleList[i].X);
                }

            }
            return weight_list_x;
        }
        public List<double> weight_list_y(double weight_number)
        {
            List<double> weight_list_y = new List<double>();
            for (int i = 0; i < particleList.Count; ++i)
            {
                if (particleList[i].W <= weight_number)
                {
                    weight_list_y.Add(particleList[i].Y);
                }

            }
            return weight_list_y;
        }

        public static void Main(string[] args)
        {
            ParticleFilter p1 = new ParticleFilter();
            p1.create();
            
            for (int i = 0; i < p1.particleList.Count; ++i) 
            {
                Console.WriteLine(p1.particleList[0].X);
            }


            
        }
    }
}