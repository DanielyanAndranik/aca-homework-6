using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    /// <summary>
    /// Describes a node for AVL tree.
    /// </summary>
    /// <typeparam name="TKey">Generic type of the key.</typeparam>
    /// <typeparam name="TValue">Generic type of the value.</typeparam>
    public class AVLNode<TKey, TValue> where TKey : IComparable<TKey>
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
        /// the height of the subtree which root is this node.
        /// </summary>
        public int Height
        {
            get
            {
                if(this.LeftChild == null && this.RightChild == null)
                {
                    return 1;
                }

                if(this.LeftChild == null)
                {
                    return 1 + this.RightChild.Height;
                }

                if (this.RightChild == null)
                {
                    return 1 + this.LeftChild.Height;
                }

                return 1 + (this.LeftChild.Height > this.RightChild.Height ? this.LeftChild.Height : this.RightChild.Height);
            }
        }

        /// <summary>
        /// The left child of the node.
        /// </summary>
        public AVLNode<TKey, TValue> LeftChild { get; set; }

        /// <summary>
        /// The right child of the node.
        /// </summary>
        public AVLNode<TKey, TValue> RightChild { get; set; }

        /// <summary>
        /// The parent of the node.
        /// </summary>
        public AVLNode<TKey, TValue> Parent { get; set; }

        /// <summary>
        /// Creates a new node.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="value">The value of the node.</param>
        public AVLNode(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            this.LeftChild = null;
            this.RightChild = null;
            this.Parent = null;
        }
    }
}
