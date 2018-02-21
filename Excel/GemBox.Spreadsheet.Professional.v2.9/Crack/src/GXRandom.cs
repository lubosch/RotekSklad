namespace GemBox
{
    using System;

    internal class GXRandom
    {
        private int inext;
        private int inextp;
        private int[] ma;
        private const int MBIG = 0x7fffffff;
        private const int MSEED = 0x9a4ec86;
        private const int MZ = 0;

        public GXRandom() : this(Environment.TickCount)
        {
        }

        public GXRandom(int Seed)
        {
            this.ma = new int[0x38];
            int num2 = 0x9a4ec86 - Math.Abs(Seed);
            this.ma[0x37] = num2;
            int num3 = 1;
            for (int i = 1; i < 0x37; i++)
            {
                int index = (0x15 * i) % 0x37;
                this.ma[index] = num3;
                num3 = num2 - num3;
                if (num3 < 0)
                {
                    num3 += 0x7fffffff;
                }
                num2 = this.ma[index];
            }
            for (int j = 1; j < 5; j++)
            {
                for (int k = 1; k < 0x38; k++)
                {
                    this.ma[k] -= this.ma[1 + ((k + 30) % 0x37)];
                    if (this.ma[k] < 0)
                    {
                        this.ma[k] += 0x7fffffff;
                    }
                }
            }
            this.inext = 0;
            this.inextp = 0x15;
        }

        public virtual int Next()
        {
            return (int) (this.Sample() * 2147483647.0);
        }

        public virtual int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException("Max value is less then min value.");
            }
            if (maxValue == 0)
            {
                return 0;
            }
            return (int) (this.Sample() * maxValue);
        }

        public virtual int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException("Min value is greater then max value.");
            }
            if (minValue == maxValue)
            {
                return minValue;
            }
            return (((int) (this.Sample() * (maxValue - minValue))) + minValue);
        }

        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte) (this.Sample() * 256.0);
            }
        }

        public virtual double NextDouble()
        {
            return this.Sample();
        }

        protected virtual double Sample()
        {
            if (++this.inext >= 0x38)
            {
                this.inext = 1;
            }
            if (++this.inextp >= 0x38)
            {
                this.inextp = 1;
            }
            int num = this.ma[this.inext] - this.ma[this.inextp];
            if (num < 0)
            {
                num += 0x7fffffff;
            }
            this.ma[this.inext] = num;
            return (num * 4.6566128752457969E-10);
        }
    }
}

