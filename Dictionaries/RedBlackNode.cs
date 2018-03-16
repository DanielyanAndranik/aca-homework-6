using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    /// <summary>
    /// Describes a node for Red Black tree.
    /// </summary>
    /// <typeparam name="TKey">Generic type of the key.</typeparam>
    /// <typeparam name="TValue">Generic type of the value.</typeparam>
    internal class RedBlackNode<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The key of the node.
        /// </summary>
        public readonly TKey Key;

        /// <summary>
        /// The value of the node.
        /// </summary>
        public TValue Value { get; set; }

        /// <summary>
        /// The color of the node.
        /// </summary>
        public Color Color { get; set;}

        /// <summary>
        /// The left child of the node.
        /// </summary>
        public RedBlackNode<TKey, TValue> LeftChild { get; set; }

        /// <summary>
        /// The right child of the node.
        /// </summary>
        public RedBlackNode<TKey, TValue> RightChild { get; set; }

        /// <summary>
        /// The parent of the node.
        /// </summary>
        public RedBlackNode<TKey, TValue> Parent { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="value">The value of the node.</param>
        public RedBlackNode(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            this.Color = Color.Red;
            this.LeftChild = null;
            this.RightChild = null;
            this.Parent = null;
        }
    }
}
