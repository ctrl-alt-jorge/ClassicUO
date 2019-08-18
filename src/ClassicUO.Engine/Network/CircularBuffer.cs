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
    using System;

    public sealed class CircularBuffer
    {
        private byte[] buffer;
        private int head;
        private int tail;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircularBuffer"/> class.
        ///     Constructs a new instance of a byte queue.
        /// </summary>
        public CircularBuffer()
        {
            buffer = new byte[0x10000];
        }

        /// <summary>
        ///     Gets the length of the byte queue.
        /// </summary>
        public int Length { get; private set; }

        public byte GetID()
        {
            if (Length >= 1)
            {
                return buffer[head];
            }

            return 0xFF;
        }

        public int GetLength()
        {
            if (Length >= 3)
            {
                return buffer[(head + 2) % buffer.Length] | (buffer[(head + 1) % buffer.Length] << 8);
            }

            // return (_buffer[(_head + 1) % _buffer.Length] << 8) | _buffer[(_head + 2) % _buffer.Length];
            return 0;
        }

        /// <summary>
        ///     Enqueues a buffer to the queue and inserts it to a correct position.
        /// </summary>
        /// <param name="buffer">Buffer to enqueue.</param>
        /// <param name="offset">The zero-based byte offset in the buffer.</param>
        /// <param name="size">The number of bytes to enqueue.</param>
        public void Enqueue(byte[] buffer, int offset, int size)
        {
            if (Length + size > this.buffer.Length)
            {
                SetCapacity((Length + size + 2047) & ~2047);
            }

            if (head < tail)
            {
                int rightLength = this.buffer.Length - tail;

                if (rightLength >= size)
                {
                    Buffer.BlockCopy(buffer, offset, this.buffer, tail, size);
                }
                else
                {
                    Buffer.BlockCopy(buffer, offset, this.buffer, tail, rightLength);
                    Buffer.BlockCopy(buffer, offset + rightLength, this.buffer, 0, size - rightLength);
                }
            }
            else
            {
                Buffer.BlockCopy(buffer, offset, this.buffer, tail, size);
            }

            tail = (tail + size) % this.buffer.Length;
            Length += size;
        }

        /// <summary>
        ///     Dequeues a buffer from the queue.
        /// </summary>
        /// <param name="buffer">Buffer to enqueue.</param>
        /// <param name="offset">The zero-based byte offset in the buffer.</param>
        /// <param name="size">The number of bytes to dequeue.</param>
        /// <returns>Number of bytes dequeued.</returns>
        public int Dequeue(byte[] buffer, int offset, int size)
        {
            if (size > Length)
            {
                size = Length;
            }

            if (size == 0)
            {
                return 0;
            }

            if (head < tail)
            {
                Buffer.BlockCopy(this.buffer, head, buffer, offset, size);
            }
            else
            {
                int rightLength = this.buffer.Length - head;

                if (rightLength >= size)
                {
                    Buffer.BlockCopy(this.buffer, head, buffer, offset, size);
                }
                else
                {
                    Buffer.BlockCopy(this.buffer, head, buffer, offset, rightLength);
                    Buffer.BlockCopy(this.buffer, 0, buffer, offset + rightLength, size - rightLength);
                }
            }

            head = (head + size) % this.buffer.Length;
            Length -= size;

            if (Length == 0)
            {
                head = 0;
                tail = 0;
            }

            return size;
        }

        /// <summary>
        ///     Clears the byte queue.
        /// </summary>
        internal void Clear()
        {
            head = 0;
            tail = 0;
            Length = 0;
        }

        /// <summary>
        ///     Extends the capacity of the bytequeue.
        /// </summary>
        private void SetCapacity(int capacity)
        {
            byte[] newBuffer = new byte[capacity];

            if (Length > 0)
            {
                if (head < tail)
                {
                    Buffer.BlockCopy(buffer, head, newBuffer, 0, Length);
                }
                else
                {
                    Buffer.BlockCopy(buffer, head, newBuffer, 0, buffer.Length - head);
                    Buffer.BlockCopy(buffer, 0, newBuffer, buffer.Length - head, tail);
                }
            }

            head = 0;
            tail = Length;
            buffer = newBuffer;
        }
    }
}