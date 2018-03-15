using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    public class AVLDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IEnumerator<KeyValuePair<TKey, TValue>> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The root node of the tree.
        /// </summary>
        private AVLNode<TKey, TValue> root;


        /// <summary>
        /// Returns all keys in the tree as ICollection. 
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                ICollection<TKey> keys = new List<TKey>();
                foreach (var item in this)
                {
                    keys.Add(item.Key);
                }
                return keys;
            }
        }

        /// <summary>
        /// Returns all values in the tree as ICollection.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                ICollection<TValue> values = new List<TValue>();
                foreach (var item in this)
                {
                    values.Add(item.Value);
                }
                return values;
            }
        }

        /// <summary>
        /// Indexer operator.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <returns>Returns value if key exists.</returns>
        public TValue this[TKey key]
        {
            get
            {
                if (this.root == null)
                {
                    throw new ArgumentNullException();
                }

                AVLNode<TKey, TValue> temporary = this.SearchNodeRecursivly(key, this.root);

                if (temporary == null)
                {
                    throw new ArgumentNullException();
                }

                return temporary.Value;
            }
            set
            {
                if (this.root == null)
                {
                    throw new ArgumentNullException();
                }

                AVLNode<TKey, TValue> temporary = this.SearchNodeRecursivly(key, this.root);

                if (temporary == null)
                {
                    throw new ArgumentNullException();
                }

                temporary.Value = value;
            }
        }

        /// <summary>
        /// The count of nodes in the tree.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Checks wheather the tree is immutable.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a new empty tree.
        /// </summary>
        public AVLDictionary()
        {
            this.root = null;
            this.Count = 0;
        }

        /// <summary>
        /// Inserts a new node into the tree.
        /// </summary>
        /// <param name="node">The node.</param>
        public void Add(TKey key, TValue value)
        {
            AVLNode<TKey, TValue> node = new AVLNode<TKey, TValue>(key, value);
            if (this.root == null)
            {
                this.root = node;
                this.Count++;
                return;
            }

            if (!this.InsertRecursivly(node, this.root))
            {
                throw new CannotUnloadAppDomainException();
            }
            this.Count++;
        }

        /// <summary>
        /// Insert a new into the tree.
        /// </summary>
        /// <param name="item">Key-value pair.</param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Checks wheather the tree contains the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Returns true if the key was found, otherwise returns false.</returns>
        public bool ContainsKey(TKey key)
        {
            if (this.root == null)
            {
                return false;
            }

            return this.SearchKeyRecursivly(key, this.root);
        }

        /// <summary>
        /// Check wheather the tree contains the key-value pair.
        /// </summary>
        /// <param name="item">The key-value pair.</param>
        /// <returns>returns true if the key-value pair exists, else returns false.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (this.root == null)
            {
                return false;
            }

            AVLNode<TKey, TValue> temporary = this.SearchNodeRecursivly(item.Key, this.root);

            if (temporary == null)
            {
                return false;
            }
            if (temporary.Value.Equals(item.Value))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a node from the tree.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <returns>Returns true if node was deleted.</returns>
        public bool Remove(TKey key)
        {
            if (this.root == null)
            {
                return false;
            }

            if (this.RemoveRecursivly(key, this.root))
            {
                this.Count--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes a node from the tree.
        /// </summary>
        /// <param name="item">Key-value pair of the tree.</param>
        /// <returns>Returns true if the node was deleted.</returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this.root == null)
            {
                return false;
            }

            AVLNode<TKey, TValue> temporary = this.SearchNodeRecursivly(item.Key, this.root);

            if (temporary == null)
            {
                return false;
            }

            this.RemoveRecursivly(temporary.Key, temporary);
            return true;
        }

        /// <summary>
        /// Makes the tree empty.
        /// </summary>
        public void Clear()
        {
            if (this.root == null)
            {
                return;
            }

            this.ClearRecursivly(this.root);
        }

        /// <summary>
        /// Tries to get value of a node.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="value">The value.</param>
        /// <returns>Returns true if key exists.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (this.root == null)
            {
                value = default(TValue);
                return false;
            }

            AVLNode<TKey, TValue> temporary = this.SearchNodeRecursivly(key, this.root);

            if (temporary == null)
            {
                value = default(TValue);
                return false;
            }

            value = temporary.Value;
            return true;
        }

        /// <summary>
        /// Copies the dictionary into the array.
        /// </summary>
        /// <param name="array">Array where should copy.</param>
        /// <param name="arrayIndex">Start index.</param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var item in this)
            {
                array[arrayIndex++] = item;
            }
        }


        /// <summary>
        /// Inserts a new node into the tree.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <param name="rootOfSubTree">The root of subtree.</param>
        /// <returns>Returns true if tne node is added, else returns false.</returns>
        private bool InsertRecursivly(AVLNode<TKey, TValue> node, AVLNode<TKey, TValue> rootOfSubTree)
        {
            if (node.Key.CompareTo(rootOfSubTree.Key) < 0)
            {
                if (rootOfSubTree.LeftChild == null)
                {
                    rootOfSubTree.LeftChild = node;
                    rootOfSubTree.LeftChild.Parent = rootOfSubTree;
                    this.CheckBalance(node);
                    return true;
                }
                return this.InsertRecursivly(node, rootOfSubTree.LeftChild);
            }
            else if (node.Key.CompareTo(rootOfSubTree.Key) > 0)
            {
                if (rootOfSubTree.RightChild == null)
                {
                    rootOfSubTree.RightChild = node;
                    rootOfSubTree.RightChild.Parent = rootOfSubTree;
                    this.CheckBalance(node);
                    return true;
                }
                return this.InsertRecursivly(node, rootOfSubTree.RightChild);
            }
            return false;
        }

        /// <summary>
        /// Searches the key in the subtree.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="node">The root of the subtree.</param>
        /// <returns>Returns true if tne node is added, else returns false.</returns>
        private bool SearchKeyRecursivly(TKey key, AVLNode<TKey, TValue> node)
        {
            if (key.CompareTo(node.Key) == 0)
            {
                return true;
            }

            if (key.CompareTo(node.Key) < 0)
            {
                if (node.LeftChild == null)
                {
                    return false;
                }
                return this.SearchKeyRecursivly(key, node.LeftChild);
            }

            if (node.RightChild == null)
            {
                return false;
            }
            return this.SearchKeyRecursivly(key, node.RightChild);
        }

        /// <summary>
        /// Searches a node in the tree.
        /// </summary>
        /// <param name="key">The key of the node.</param>
        /// <param name="rootOfSubtree">The root node of the subtree.</param>
        /// <returns>Returns node if it exists, otherwise null.</returns>
        private AVLNode<TKey, TValue> SearchNodeRecursivly(TKey key, AVLNode<TKey, TValue> rootOfSubtree)
        {
            if (key.CompareTo(rootOfSubtree.Key) < 0)
            {
                if (rootOfSubtree.LeftChild == null)
                {
                    return null;
                }
                return this.SearchNodeRecursivly(key, rootOfSubtree.LeftChild);
            }

            if (key.CompareTo(rootOfSubtree.Key) > 0)
            {
                if (rootOfSubtree.RightChild == null)
                {
                    return null;
                }
                return this.SearchNodeRecursivly(key, rootOfSubtree.RightChild);
            }

            return rootOfSubtree;
        }

        /// <summary>
        /// Checks wheather the tree is balanced.
        /// </summary>
        /// <param name="node"></param>
        private void CheckBalance(AVLNode<TKey, TValue> node)
        {

            if (node.Height <= 2)
            {
                if (node.Parent != null)
                {
                    this.CheckBalance(node.Parent);
                }
                return;
            }

            if (node.LeftChild == null)
            {
                if (node.RightChild.RightChild != null)
                {
                    this.LeftRotation(node);
                }
                else
                {
                    this.RightLeftRotation(node);
                }
                if (node.Parent != null)
                {
                    this.CheckBalance(node.Parent);
                }
                return;
            }
            else if (node.RightChild == null)
            {
                if (node.LeftChild.LeftChild != null)
                {
                    this.RightRotation(node);
                }
                else
                {
                    this.LeftRightRotation(node);
                }
                if (node.Parent != null)
                {
                    this.CheckBalance(node.Parent);
                }
                return;
            }
            if (node.LeftChild.Height - node.RightChild.Height > 1)
            {
                this.RightRotation(node);
            }
            else if (node.LeftChild.Height - node.RightChild.Height < -1)
            {
                this.LeftRotation(node);
            }
            if (node.Parent != null)
            {
                this.CheckBalance(node.Parent);
            }
        }

        /// <summary>
        /// Rotates the node left.
        /// </summary>
        /// <param name="node">The node.</param>
        private void LeftRotation(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> temporary = node.RightChild;

            node.RightChild = temporary.LeftChild;
            if (temporary.LeftChild != null)
            {
                temporary.LeftChild.Parent = node;
            }
            if (node.Parent == null)
            {
                temporary.Parent = null;
                this.root = temporary;
            }
            else
            {
                temporary.Parent = node.Parent;
                if (node.Parent.LeftChild == node)
                {
                    node.Parent.LeftChild = temporary;
                }
                else
                {
                    node.Parent.RightChild = temporary;
                }
            }
            temporary.LeftChild = node;
            node.Parent = temporary;
        }

        /// <summary>
        /// Rotates the node right.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RightRotation(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> temporary = node.LeftChild;

            node.LeftChild = temporary.RightChild;
            if (temporary.RightChild != null)
            {
                temporary.RightChild.Parent = node;
            }
            if (node.Parent == null)
            {
                temporary.Parent = null;
                this.root = temporary;
            }
            else
            {
                temporary.Parent = node.Parent;
                if (node.Parent.LeftChild == node)
                {
                    node.Parent.LeftChild = temporary;
                }
                else
                {
                    node.Parent.RightChild = temporary;
                }
            }
            temporary.RightChild = node;
            node.Parent = temporary;
        }

        /// <summary>
        /// Makes two rotations, at first rotate left the nodes child, then do right the node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void LeftRightRotation(AVLNode<TKey, TValue> node)
        {
            this.LeftRotation(node.LeftChild);
            this.RightRotation(node);
        }

        /// <summary>
        /// Makes two rotations, at first rotate left the nodes right child, then do left the node.
        /// </summary>
        /// <param name="node">The node.</param>
        private void RightLeftRotation(AVLNode<TKey, TValue> node)
        {
            RightRotation(node.RightChild);
            LeftRotation(node);
        }

        /// <summary>
        /// Finds a node recursivly and removes it.
        /// </summary>
        /// <param name="key">The key of node.</param>
        /// <param name="rootOfSubtree">The root node of subtree.</param>
        /// <returns>returns true if the node was deleted.</returns>
        private bool RemoveRecursivly(TKey key, AVLNode<TKey, TValue> rootOfSubtree)
        {
            if (key.CompareTo(rootOfSubtree.Key) > 0)
            {
                if (rootOfSubtree.RightChild != null)
                {
                    return this.RemoveRecursivly(key, rootOfSubtree.RightChild);
                }
                return false;
            }

            if (key.CompareTo(rootOfSubtree.Key) < 0)
            {
                if (rootOfSubtree.LeftChild != null)
                {
                    return this.RemoveRecursivly(key, rootOfSubtree.LeftChild);
                }
                return false;
            }

            if (rootOfSubtree.LeftChild == null && rootOfSubtree.RightChild == null)
            {
                this.RemoveLeaf(rootOfSubtree);
                return true;
            }

            if (rootOfSubtree.LeftChild == null)
            {
                this.RemoveNodeHavingRightChild(rootOfSubtree);
                //this.CheckViolation(rootOfSubtree.RightChild);
                return true;
            }

            if (rootOfSubtree.RightChild == null)
            {
                this.RemoveNodeHavingLeftChild(rootOfSubtree);
                // this.CheckViolation(rootOfSubtree.LeftChild);
                return true;
            }


            this.RemoveNode(rootOfSubtree);
            //this.CheckViolation(rootOfSubtree);
            return true;
        }

        /// <summary>
        /// Removes a leaf from the tree.
        /// </summary>
        /// <param name="node">The node which will be removed.</param>
        private void RemoveLeaf(AVLNode<TKey, TValue> node)
        {
            if (node.Parent == null)
            {
                node = null;
                this.Count--;
                return;
            }
            if (node == node.Parent.LeftChild)
            {
                node.Parent.LeftChild = null;
                this.Count--;
                return;
            }
            node.Parent.RightChild = null;
            this.Count--;
            return;
        }

        /// <summary>
        /// Removes a node which has only left child.
        /// </summary>
        /// <param name="node">The node which will be removed.</param>
        private void RemoveNodeHavingLeftChild(AVLNode<TKey, TValue> node)
        {
            if (node.Parent == null)
            {
                this.root = node.LeftChild;
                root.Parent = null;
                this.Count--;
                return;
            }
            if (node == node.Parent.LeftChild)
            {
                node.Parent.LeftChild = node.LeftChild;
                node.LeftChild.Parent = node.Parent;
                this.Count--;
                return;
            }
            node.Parent.RightChild = node.LeftChild;
            node.LeftChild.Parent = node.Parent;
            this.Count--;
            return;
        }

        /// <summary>
        /// Removes a node which has only right child.
        /// </summary>
        /// <param name="node">The node which will be removed.</param>
        private void RemoveNodeHavingRightChild(AVLNode<TKey, TValue> node)
        {
            if (node.Parent == null)
            {
                this.root = node.RightChild;
                root.Parent = null;
                this.Count--;
                return;
            }
            if (node == node.Parent.LeftChild)
            {
                node.Parent.LeftChild = node.RightChild;
                node.RightChild.Parent = node.Parent;
                this.Count--;
                return;
            }
            node.Parent.RightChild = node.RightChild;
            node.RightChild.Parent = node.Parent;
            this.Count--;
            return;
        }

        /// <summary>
        /// Removes the node which has both left and right children.
        /// </summary>
        /// <param name="node">The node which will be removed.</param>
        private void RemoveNode(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> incomer = this.InOrderSuccessor(node);

            if (incomer == node.RightChild)
            {
                node.RightChild = incomer.RightChild;
            }
            else
            {
                incomer.Parent.LeftChild = null;
            }

            incomer = node;
            if (node.Parent == null)
            {
                this.root = incomer;
            }
            else
            {
                if (node == node.Parent.LeftChild)
                {
                    node.Parent.LeftChild = incomer;
                }
                else
                {
                    node.Parent.RightChild = incomer;
                }
            }

            this.Count--;
            return;
        }

        /// <summary>
        /// Clear the tree recurisvly.
        /// </summary>
        /// <param name="rootOfSubtree">The root node of subtree.</param>
        private void ClearRecursivly(AVLNode<TKey, TValue> rootOfSubtree)
        {
            if (rootOfSubtree.LeftChild != null)
            {
                this.ClearRecursivly(rootOfSubtree.LeftChild);
            }

            if (rootOfSubtree.RightChild != null)
            {
                this.ClearRecursivly(rootOfSubtree.RightChild);
            }
            rootOfSubtree = null;
        }

        /// <summary>
        /// Returns the next node.
        /// </summary>
        /// <param name="node">The current node.</param>
        /// <returns></returns>
        private AVLNode<TKey, TValue> InOrderSuccessor(AVLNode<TKey, TValue> node)
        {
            AVLNode<TKey, TValue> temporary;
            if (node.RightChild == null)
            {
                if (node.Parent == null)
                {
                    return null;
                }

                temporary = node;

                while (temporary.Parent != null)
                {
                    if (temporary == temporary.Parent.RightChild)
                    {
                        temporary = temporary.Parent;
                    }
                    else
                    {
                        break;
                    }
                }
                return temporary.Parent;

            }
            else
            {
                temporary = node.RightChild;
                while (temporary.LeftChild != null)
                {
                    temporary = temporary.LeftChild;
                }
                return temporary;
            }

        }

        /// <summary>
        /// Pointer node, to iterate through the tree.
        /// </summary>
        private AVLNode<TKey, TValue> current;

        /// <summary>
        /// Key-value pair pointer; 
        /// </summary>
        private KeyValuePair<TKey, TValue> currentPair;

        /// <summary>
        /// Returns the pointer as KeyValuePair.
        /// </summary>
        KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current
        {
            get
            {
                return this.currentPair;
            }
        }

        /// <summary>
        /// Returns the pointer as object.
        /// </summary>
        object IEnumerator.Current
        {
            get
            {
                return this.current;
            }
        }

        /// <summary>
        /// Returns Enumertor.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// returns Enumerator.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Moves the pointer to the next position.
        /// </summary>
        /// <returns>Returns true if the pointer was moved successfully.</returns>
        public bool MoveNext()
        {
            if (this.root == null)
            {
                return false;
            }

            if (this.current == null)
            {
                this.Reset();
                return true;
            }

            this.current = this.InOrderSuccessor(this.current);
            if (current == null)
            {
                return false;
            }
            this.currentPair = new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
            return true;
        }

        /// <summary>
        /// Resets the pointer.
        /// </summary>
        public void Reset()
        {
            this.current = this.root;

            while (this.current.LeftChild != null)
            {
                this.current = this.current.LeftChild;
            }
            this.currentPair = new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RedBlackDictionary() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
