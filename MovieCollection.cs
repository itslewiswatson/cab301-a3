// Phase 2
// An implementation of MovieCollection ADT
// 2022


using System;

//A class that models a node of a binary search tree
//An instance of this class is a node in a binary search tree 
public class BTreeNode
{
	private IMovie movie; // movie
	private BTreeNode lchild; // reference to its left child 
	private BTreeNode rchild; // reference to its right child

	public BTreeNode(IMovie movie)
	{
		this.movie = movie;
		lchild = null;
		rchild = null;
	}

	public IMovie Movie
	{
		get { return movie; }
		set { movie = value; }
	}

	public BTreeNode LChild
	{
		get { return lchild; }
		set { lchild = value; }
	}

	public BTreeNode RChild
	{
		get { return rchild; }
		set { rchild = value; }
	}
}

// invariant: no duplicates in this movie collection
public class MovieCollection : IMovieCollection
{
	private BTreeNode root; // movies are stored in a binary search tree and the root of the binary search tree is 'root' 
	private int count; // the number of (different) movies currently stored in this movie collection 



	// get the number of movies in this movie colllection 
	// pre-condition: nil
	// post-condition: return the number of movies in this movie collection and this movie collection remains unchanged
	public int Number { get { return count; } }

	// constructor - create an object of MovieCollection object
	public MovieCollection()
	{
		root = null;
		count = 0;	
	}

	// Check if this movie collection is empty
	// Pre-condition: nil
	// Post-condition: return true if this movie collection is empty; otherwise, return false.
	public bool IsEmpty()
	{
		return root == null;
	}

	// Insert a movie into this movie collection
	// Pre-condition: nil
	// Post-condition: the movie has been added into this movie collection and return true, if the movie is not in this movie collection; otherwise, the movie has not been added into this movie collection and return false.
	public bool Insert(IMovie movie)
	{
		if (Search(movie))
        {
			return false;
        }

		if (root == null)
        {
			count++;
			root = new BTreeNode(movie);
			return true;
        }

		return Insert(movie, root);
	}

	// Insert a movie into a binary tree at its correctly sorted location
	// Pre-condition: nil
	// Post-condition: the movie is added to the collection and returned true or it returns the recursive value of itself on a different node
	private bool Insert(IMovie movie, BTreeNode r)
    {
		if (movie.CompareTo(r.Movie) < 0)
		{
			if (r.LChild == null)
			{
				r.LChild = new BTreeNode(movie);
				count++;
				return true;
			}
			else
			{
				return Insert(movie, r.LChild);
			}
		}
		else
		{
			if (r.RChild == null)
			{
				r.RChild = new BTreeNode(movie);
				count++;
				return true;
			}
			else
			{
				return Insert(movie, r.RChild);
			}
		}
	}


	// Delete a movie from this movie collection
	// Pre-condition: nil
	// Post-condition: the movie is removed out of this movie collection and return true, if it is in this movie collection; return false, if it is not in this movie collection
	public bool Delete(IMovie movie)
	{
		BTreeNode r = root;
		BTreeNode parent = null;

		while (r != null && movie.CompareTo(r.Movie) != 0)
        {
			parent = r;
			if (movie.CompareTo(r.Movie) < 0)
            {
				r = r.LChild;
            }
			else
            {
				r = r.RChild;
            }
        }

		if (r == null)
        {
			return false;
        }

		if (r.LChild != null && r.RChild != null)
		{
			if (r.LChild.RChild == null)
			{
				r.Movie = r.LChild.Movie;
				r.LChild = r.LChild.LChild;
			}
			else
			{
				BTreeNode p = r.LChild;
				BTreeNode pp = r;
				while (p.RChild != null)
				{
					pp = p;
					p = p.RChild;
				}

				r.Movie = p.Movie;
				pp.RChild = p.LChild;
			}
		}
		else
		{
			BTreeNode c;
			if (r.LChild != null)
				c = r.LChild;
			else
				c = r.RChild;

			if (r == root)
				root = c;
			else
			{
				if (r == parent.LChild)
					parent.LChild = c;
				else
					parent.RChild = c;
			}
		}

		count--;
		return true;
	}

	// Search for a movie in this movie collection
	// pre: nil
	// post: return true if the movie is in this movie collection;
	//	     otherwise, return false.
	public bool Search(IMovie movie)
	{
		return Search(movie, root);
	}

	// Search for a movie against a tree node
	// pre: nil
	// post: return true if the movie matches the current node;
	//	     otherwise, return the recursive result of a neighbouring node.
	private bool Search(IMovie movie, BTreeNode r)
    {
		if (r == null)
			return false;

		if (movie.CompareTo(r.Movie) == 0)
		{
			return true;
		}
		else if (movie.CompareTo(r.Movie) < 0)
		{
			return Search(movie, r.LChild);
		}
		else
		{
			return Search(movie, r.RChild);
		}
	}

	// Search for a movie by its title in this movie collection  
	// pre: nil
	// post: return the reference of the movie object if the movie is in this movie collection;
	//	     otherwise, return null.
	public IMovie Search(string movietitle)
	{
		if (root == null) return null;

		return Search(movietitle, root);
	}

	// Search for a movie by its title against a binary tree node
	// pre: nil
	// post: return true if the movie matches the current node;
	//	     otherwise, return the recursive result of a neighbouring node.
	private IMovie Search(string movietitle, BTreeNode r)
    {
		if (r == null) return null;

		if (movietitle.CompareTo(r.Movie.Title) == 0)
		{
			return r.Movie;
		}
		else if (movietitle.CompareTo(r.Movie.Title) < 0)
		{
			return Search(movietitle, r.LChild);
		}
		else
		{
			return Search(movietitle, root.RChild);
		}
	}

	// Store all the movies in this movie collection in an array in the dictionary order by their titles
	// Pre-condition: nil
	// Post-condition: return an array of movies that are stored in dictionary order by their titles
	public IMovie[] ToArray()
	{
		if (count == 0) return new IMovie[0];

		IMovie[] movies = new IMovie[count + 1];

		int i = 0;
		Inorder(root, ref movies, ref i);

		return movies;
	}

	// Binary in-order tree traversal
	// Pre-condition: r is an ordered binary tree, movies is an empty array of type IMovie and iter is an integer equal to or greater than zero
	// Post-condition: movies array contains every node from the binary tree r in order
	private void Inorder(BTreeNode r, ref IMovie[] movies, ref int iter)
    {
		if (r != null)
        {
			Inorder(r.LChild, ref movies, ref iter);
			movies[iter++] = r.Movie;
			Inorder(r.RChild, ref movies, ref iter);
		}
	}

	// Clear this movie collection
	// Pre-condotion: nil
	// Post-condition: all the movies have been removed from this movie collection 
	public void Clear()
	{
		count = 0;
		root = null;
	}
}
