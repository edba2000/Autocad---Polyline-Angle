internal class DirLen
    {
        double _angle;
        double _len;

        /// <summary>
        ///     angle - double.  Default value is 0.
        /// </summary>
        public double angle
        {
            get
            {
                return _angle;
            }

            set
            {
                _angle = value;
            }
        }

        /// <summary>
        ///     angle - double.  Default value is 0.
        /// </summary>
        public double len
        {
            get
            {
                return _len;
            }

            set
            {
                _len = value;
            }
        }

        public DirLen() 
        { 
            _len = 0.0; 
        }

        public DirLen(double angle, double len)
        {
            _angle = angle;
            _len = len;
        }
