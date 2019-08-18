// Copyright (C) 2019 ClassicUO Development Community on Github.
//
// This project is an alternative client for the game Ultima Online.
// The goal of this is to develop a lightweight client considering
//  new technologies.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <https://www.gnu.org/licenses/>.

namespace ClassicUO.Network
{
    using System.Collections.Generic;

    public class BufferPool
    {
        private readonly int arraySize;
        private readonly int capacity;
        private readonly Queue<byte[]> freeSegment;

        public BufferPool(int capacity, int arraysize)
        {
            this.capacity = capacity;
            arraySize = arraysize;
            freeSegment = new Queue<byte[]>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                freeSegment.Enqueue(new byte[arraysize]);
            }
        }

        public byte[] GetFreeSegment()
        {
            lock (this)
            {
                if (freeSegment.Count > 0)
                {
                    return freeSegment.Dequeue();
                }

                for (int i = 0; i < capacity; i++)
                {
                    freeSegment.Enqueue(new byte[arraySize]);
                }

                return freeSegment.Dequeue();
            }
        }

        public void AddFreeSegment(byte[] segment)
        {
            if (segment == null)
            {
                return;
            }

            lock (this)
            {
                freeSegment.Enqueue(segment);
            }
        }
    }
}