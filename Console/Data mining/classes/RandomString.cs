using System;
using System.Collections;

namespace Util {
    class RandomString {
        # region variables
        static private string randomTEXT = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!#?=+-*^%&()";
        static private Random rand = new Random();
        static private Tree tree = null;

        # endregion

        static private char newString () {
            return randomTEXT[rand.Next(randomTEXT.Length)];
        }

        static public string Generate (int size = 25) {
            if (tree == null) {
                tree = new Tree(size);
            }
            string str = tree.Next();
            if (str != null) {
                return str;
            } else {
                tree = new Tree(size);
                return Generate(size);
            }
        }



        private class Node {
            # region variables
            private Node right;
            private Node left;
            private int left_pointer;
            private int right_pointer;
            public bool marked = false;
            public bool dead = false;
            private char [] array;
            #endregion

            # region getters
            public Node Left {
                get { return left; }
            }
            public Node Right {
                get { return right; }
            }
            # endregion

            public Node (char [] array, int left, int right) {
                this.left_pointer = left;
                this.right_pointer = right;
                this.left = null;
                this.right = null;
                this.array = array;
                for (int i = left; i < right; i++) {
                    this.array[i] = RandomString.newString();
                }
            }

            public override string ToString () {
                if (this.marked) {
                    this.dead = true;
                    Array.Reverse( array );
                    return new string( array );
                }
                else {
                    this.marked = true;
                    return new string ( array ); 
                }
            }
            
            public Node[] nextGen () {
                if (this.right_pointer - this.left_pointer < 3) {
                    return Array.Empty<Node>();
                }

                int middle = (this.left_pointer + this.right_pointer) / 2;
                left = new Node(array, left_pointer, middle);
                right = new Node(array, middle, right_pointer);
                return new Node[] { left, right };
            }
        }
        private class Tree {
            private Node root;
            private Stack stack;

            public Tree (int size) {
                this.root = new Node(new char[size], 0, size);
                this.stack = new Stack();
                this.stack.Push(this.root);
            }

            public String Next () {
                if (this.root.dead && this.stack.Count == 0) {
                    return null;
                }

                Node target = (Node)this.stack.Pop();
                if (target.marked) {
                    if (target.Left != null) {
                        this.stack.Push(target.Left);
                        this.stack.Push(target.Right);
                    }
                } else {
                    Node[] list = target.nextGen();
                    if (list.Length > 0) {
                        this.stack.Push(list[0]);
                        this.stack.Push(list[1]);
                    } else if (this.stack.Count == 0) {
                        this.stack.Push(this.root);
                    }
                }

                return target.ToString();
            }
        }
    }
}