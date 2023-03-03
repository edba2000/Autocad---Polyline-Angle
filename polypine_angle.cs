        /// <summary>
        /// Get the rotation angle of a polyline in degrees. The return value is rouded to 2 decimal places.
        /// The check is done on the longest line segment. All arcs are ignored.
        /// 
        /// </summary>
        /// <param name="pLine">Input Polyline object</param>
        /// <param name="rotAngle">Output polyline angle value</param>
        /// <param name="maxSegmentLength">Input Polyline object</param>
        /// <param name="minX">Min polyline X (vertex value)</param>
        /// <param name="maxX">Max polyline X (vertex value)</param>
        /// <param name="minY">Min polyline Y (vertex value)</param>
        /// <param name="maxY">Max polyline Y (vertex value)</param>
        /// <returns>void</returns>
        private void getPLineRotAngle(AcDb.Polyline pLine, ref double rotAngle, ref double maxSegmentLength, ref double minX, ref double maxX, ref double minY, ref double maxY)
        {
            List<DirLen> dirLen = new List<DirLen>();

            double angle = 0.0;
            double len = 0.0;
            bool found;

            if (pLine.NumberOfVertices > 1)
            {
                int j;
                for (int i = 0; i < (pLine.Closed ? pLine.NumberOfVertices : pLine.NumberOfVertices - 1); i++)
                {
                    if (pLine.GetBulgeAt(i) == 0.0)
                    {
                        if (i == (pLine.NumberOfVertices - 1)) j = 0; else j = i + 1;

                        len = Distance2Points(pLine.GetPoint2dAt(i).X, pLine.GetPoint2dAt(i).Y, pLine.GetPoint2dAt(j).X, pLine.GetPoint2dAt(j).Y);

                        angle = AngleLineCCW(pLine.GetPoint2dAt(i).X, pLine.GetPoint2dAt(i).Y, pLine.GetPoint2dAt(j).X, pLine.GetPoint2dAt(j).Y);
                        angle = Math.Round(angle, 2);
                        found = false;
                        foreach (DirLen dl in dirLen)
                        {
                            if (dl.angle == angle)
                            {
                                dl.len += len;
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            dirLen.Add(new DirLen(angle, len));
                        }
                        if (pLine.GetPoint2dAt(i).X < minX) minX = pLine.GetPoint2dAt(i).X;
                        if (pLine.GetPoint2dAt(i).Y < minY) minY = pLine.GetPoint2dAt(i).Y;
                        if (pLine.GetPoint2dAt(j).X < minX) minX = pLine.GetPoint2dAt(j).X;
                        if (pLine.GetPoint2dAt(j).Y < minY) minY = pLine.GetPoint2dAt(j).Y;

                        if (pLine.GetPoint2dAt(i).X > maxX) maxX = pLine.GetPoint2dAt(i).X;
                        if (pLine.GetPoint2dAt(i).Y > maxY) maxY = pLine.GetPoint2dAt(i).Y;
                        if (pLine.GetPoint2dAt(j).X > maxX) maxX = pLine.GetPoint2dAt(j).X;
                        if (pLine.GetPoint2dAt(j).Y > maxY) maxY = pLine.GetPoint2dAt(j).Y;
                    }
                }
                len = 0.0;
                angle = 0.0;
                foreach (DirLen dl in dirLen)
                {
                    if (dl.len > len)
                    {
                        len = dl.len;
                        angle = dl.angle;
                    }
                }
                if (len > maxSegmentLength)
                {
                    maxSegmentLength = len;
                    rotAngle = angle;
                }
            }
            dirLen.Clear();
        }
        
        private double Distance2Points(double p1x, double p1y, double p2x, double p2y)
        {
            return Math.Round(Math.Sqrt(Math.Pow((p1x - p2x), 2) + Math.Pow((p1y - p2y), 2)), 1);
        }

        private double AngleLineCCW(double x1, double y1, double x2, double y2)
        {
            double angle;
            if (x1 >= x2)
                angle =  Math.Atan2(y2 - y1, x2 - x1) * 180.0 / Math.PI;
            else
                angle = Math.Atan2(y1 - y2, x1 - x2) * 180.0 / Math.PI;

            if (angle < 0.0) angle += 360.0;
            return angle;
        }


        
