// HashtableTest.cs - NUnit Test Cases for the System.Collections.Hashtable class
//
//
// (C) Ximian, Inc.  http://www.ximian.com
// 


using System;
using System.Collections;

using NUnit.Framework;



namespace MonoTests.System.Collections {


/// <summary>Hashtable test.</summary>
public class HashtableTest : TestCase {
	public static ITest Suite {
		get {
			TestSuite suite = new TestSuite();
			
			suite.AddTest(new TestSuite(typeof(HashtableTest)));
			suite.AddTest(new TestSuite(typeof(HashtableTest2)));
			return suite;
		}
	}
	
	public void TestCtor1() {
		Hashtable h = new Hashtable();
		AssertNotNull("No hash table", h);
	}
	public void TestCtor2() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable(null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null hashtable error not thrown", 
			       errorThrown);
		}
		{
			string[] keys = {"this", "is", "a", "test"};
			char[] values = {'a', 'b', 'c', 'd'};
			Hashtable h1 = new Hashtable();
			for (int i = 0; i < keys.Length; i++) {
				h1[keys[i]] = values[i];
			}
			Hashtable h2 = new Hashtable(h1);
			for (int i = 0; i < keys.Length; i++) {
				AssertEquals("No match for key " + keys[i],
					     values[i], h2[keys[i]]);
			}
		}
	}
	// TODO - Ctors for capacity and load (how to test? any access?)
        // TODO - Ctors with IComparer, IHashCodeProvider, Serialization
	
	public void TestCount() {
		Hashtable h = new Hashtable();
		AssertEquals("new table - count zero", 0, h.Count);
		int max = 100;
		for (int i = 1; i <= max; i++) {
			h[i] = i;
			AssertEquals("Count wrong for " + i,
				     i, h.Count);
		}
		for (int i = 1; i <= max; i++) {
			h[i] = i * 2;
			AssertEquals("Count shouldn't change at " + i,
				     max, h.Count);
		}
	}

	public void TestIsFixedSize() {
		Hashtable h = new Hashtable();
		AssertEquals("hashtable not fixed by default",
			     false, h.IsFixedSize);
		// TODO - any way to get a fixed-size hashtable?
	}

	public void TestIsReadOnly() {
		Hashtable h = new Hashtable();
		AssertEquals("hashtable not read-only by default",
			     false, h.IsReadOnly);
		// TODO - any way to get a read-only hashtable?
	}

	public void TestIsSynchronized() {
		Hashtable h = new Hashtable();
		Assert("hashtable not synced by default", !h.IsSynchronized);
		Hashtable h2 = Hashtable.Synchronized(h);
		Assert("hashtable should by synced", h2.IsSynchronized);
	}

	public void TestItem() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				Object o = h[null];
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null hashtable error not thrown", 
			       errorThrown);
		}
		// TODO - if read-only and/or fixed-size is possible,
		//        test 'NotSupportedException' here

		{
			Hashtable h = new Hashtable();
			int max = 100;
			for (int i = 1; i <= max; i++) {
				h[i] = i;
				AssertEquals("value wrong for " + i,
					     i, h[i]);
			}
		}
	}

	public void TestKeys() {
		string[] keys = {"this", "is", "a", "test"};
		char[] values1 = {'a', 'b', 'c', 'd'};
		char[] values2 = {'e', 'f', 'g', 'h'};
		Hashtable h1 = new Hashtable();
		for (int i = 0; i < keys.Length; i++) {
			h1[keys[i]] = values1[i];
		}
		AssertEquals("keys wrong size",
			     keys.Length, h1.Keys.Count);
		for (int i = 0; i < keys.Length; i++) {
			h1[keys[i]] = values2[i];
		}
		AssertEquals("keys wrong size 2",
			     keys.Length, h1.Keys.Count);
	}

	// TODO - SyncRoot

	public void TestValues() {
		string[] keys = {"this", "is", "a", "test"};
		char[] values1 = {'a', 'b', 'c', 'd'};
		char[] values2 = {'e', 'f', 'g', 'h'};
		Hashtable h1 = new Hashtable();
		for (int i = 0; i < keys.Length; i++) {
			h1[keys[i]] = values1[i];
		}
		AssertEquals("values wrong size",
			     keys.Length, h1.Values.Count);
		for (int i = 0; i < keys.Length; i++) {
			h1[keys[i]] = values2[i];
		}
		AssertEquals("values wrong size 2",
			     keys.Length, h1.Values.Count);
	}
	
	public void TestAdd() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h.Add(null, "huh?");
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null add error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h.Add('a', 1);
				h.Add('a', 2);
			} catch (ArgumentException) {
				errorThrown = true;
			}
			Assert("re-add error not thrown", 
			       errorThrown);
		}
		// TODO - hit NotSupportedException
		{
			Hashtable h = new Hashtable();
			int max = 100;
			for (int i = 1; i <= max; i++) {
				h.Add(i, i);
				AssertEquals("value wrong for " + i,
					     i, h[i]);
			}
		}
	}

	public void TestClear() {
		// TODO - hit NotSupportedException
		Hashtable h = new Hashtable();
		AssertEquals("new table - count zero", 0, h.Count);
		int max = 100;
		for (int i = 1; i <= max; i++) {
			h[i] = i;
		}
		Assert("table don't gots stuff", h.Count > 0);
		h.Clear();
		AssertEquals("Table should be cleared",
			     0, h.Count);
	}

	public void TestClone() {
		{
			char[] c1 = {'a', 'b', 'c'};
			char[] c2 = {'d', 'e', 'f'};
			Hashtable h1 = new Hashtable();
			for (int i = 0; i < c1.Length; i++) {
				h1[c1[i]] = c2[i];
			}
			Hashtable h2 = (Hashtable)h1.Clone();
			AssertNotNull("got no clone!", h2);
			AssertNotNull("clone's got nothing!", h2[c1[0]]);
			for (int i = 0; i < c1.Length; i++) {
				AssertEquals("Hashtable match", 
					     h1[c1[i]], h2[c1[i]]);
			}
		}
		{
			char[] c1 = {'a', 'b', 'c'};
			char[] c20 = {'1', '2'};
			char[] c21 = {'3', '4'};
			char[] c22 = {'5', '6'};
			char[][] c2 = {c20, c21, c22};
			Hashtable h1 = new Hashtable();
			for (int i = 0; i < c1.Length; i++) {
				h1[c1[i]] = c2[i];
			}
			Hashtable h2 = (Hashtable)h1.Clone();
			AssertNotNull("got no clone!", h2);
			AssertNotNull("clone's got nothing!", h2[c1[0]]);
			for (int i = 0; i < c1.Length; i++) {
				AssertEquals("Hashtable match", 
					     h1[c1[i]], h2[c1[i]]);
			}

			((char[])h1[c1[0]])[0] = 'z';
			AssertEquals("shallow copy", h1[c1[0]], h2[c1[0]]);
		}
	}

	public void TestContains() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				bool result = h.Contains(null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null add error not thrown", 
			       errorThrown);
		}
		{
			Hashtable h = new Hashtable();
			h['a'] = "blue";
			Assert("'a'? it's in there!", h.Contains('a'));
			Assert("'b'? no way!", !h.Contains('b'));
		}
	}

	public void TestContainsKey() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				bool result = h.ContainsKey(null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null add error not thrown", 
			       errorThrown);
		}
		{
			Hashtable h = new Hashtable();
			h['a'] = "blue";
			Assert("'a'? it's in there!", h.ContainsKey('a'));
			Assert("'b'? no way!", !h.ContainsKey('b'));
		}
	}
	
	public void TestContainsValue() {
		{
			Hashtable h = new Hashtable();
			h['a'] = "blue";
			Assert("blue? it's in there!", 
			       h.ContainsValue("blue"));
			Assert("green? no way!", 
			       !h.ContainsValue("green"));
		}
	}

	public void TestCopyTo() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h.CopyTo(null, 0);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null hashtable error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				Object[] o = new Object[1];
				h.CopyTo(o, -1);
			} catch (ArgumentOutOfRangeException) {
				errorThrown = true;
			}
			Assert("out of range error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				Object[,] o = new Object[1,1];
				h.CopyTo(o, 1);
			} catch (ArgumentException) {
				errorThrown = true;
			}
			Assert("multi-dim array error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h['a'] = 1; // no error if table is empty
				Object[] o = new Object[5];
				h.CopyTo(o, 5);
			} catch (ArgumentException) {
				errorThrown = true;
			}
			Assert("no room in array error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h['a'] = 1;
				h['b'] = 2;
				h['c'] = 2;
				Object[] o = new Object[2];
				h.CopyTo(o, 0);
			} catch (ArgumentException) {
				errorThrown = true;
			}
			Assert("table too big error not thrown", 
			       errorThrown);
		}
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h["blue"] = 1;
				h["green"] = 2;
				h["red"] = 3;
				Char[] o = new Char[3];
				h.CopyTo(o, 0);
			} catch (InvalidCastException) {
				errorThrown = true;
			}
			Assert("invalid cast error not thrown", 
			       errorThrown);
		}

		{
			Hashtable h = new Hashtable();
			h['a'] = 1;
			h['b'] = 2;
			DictionaryEntry[] o = new DictionaryEntry[2];
			h.CopyTo(o,0);
			AssertEquals("first copy fine.", 'a', o[0].Key);
			AssertEquals("first copy fine.", 1, o[0].Value);
			AssertEquals("second copy fine.", 'b', o[1].Key);
			AssertEquals("second copy fine.", 2, o[1].Value);
		}
	}

	public void TestGetEnumerator() {
		String[] s1 = {"this", "is", "a", "test"};
		Char[] c1 = {'a', 'b', 'c', 'd'};
		Hashtable h1 = new Hashtable();
		for (int i = 0; i < s1.Length; i++) {
			h1[s1[i]] = c1[i];
		}
		IDictionaryEnumerator en = h1.GetEnumerator();
		AssertNotNull("No enumerator", en);
		
		for (int i = 0; i < s1.Length; i++) {
			en.MoveNext();
			Assert("Not enumerating for " + en.Key, 
			       Array.IndexOf(s1, en.Key) >= 0);
			Assert("Not enumerating for " + en.Value, 
			       Array.IndexOf(c1, en.Value) >= 0);
		}
	}

	// TODO - GetObjectData
	// TODO - OnDeserialization

	public void TestRemove() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = new Hashtable();
				h.Remove(null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null hashtable error not thrown", 
			       errorThrown);
		}
		{
			string[] keys = {"this", "is", "a", "test"};
			char[] values = {'a', 'b', 'c', 'd'};
			Hashtable h = new Hashtable();
			for (int i = 0; i < keys.Length; i++) {
				h[keys[i]] = values[i];
			}
			AssertEquals("not enough in table",
				     4, h.Count);
			h.Remove("huh?");
			AssertEquals("not enough in table",
				     4, h.Count);
			h.Remove("this");
			AssertEquals("Wrong count in table",
				     3, h.Count);
			h.Remove("this");
			AssertEquals("Wrong count in table",
				     3, h.Count);
		}
	}

	public void TestSynchronized() {
		{
			bool errorThrown = false;
			try {
				Hashtable h = Hashtable.Synchronized(null);
			} catch (ArgumentNullException) {
				errorThrown = true;
			}
			Assert("null hashtable error not thrown", 
			       errorThrown);
		}
		{
			Hashtable h = new Hashtable();
			Assert("hashtable not synced by default", 
			       !h.IsSynchronized);
			Hashtable h2 = Hashtable.Synchronized(h);
			Assert("hashtable should by synced", 
			       h2.IsSynchronized);
		}
	}
	
	
	protected Hashtable ht;
	private static Random rnd;
	
	public HashtableTest(String name) : base(name) {}
	
	protected override void SetUp() {
		ht=new Hashtable();
		rnd=new Random();
	}
	
	private void SetDefaultData() {
		ht.Clear();
		ht.Add("k1","another");
		ht.Add("k2","yet");
		ht.Add("k3","hashtable");
	}
	
	
	public void TestAddRemoveClear() {
		ht.Clear();
		Assert(ht.Count==0);
		
		SetDefaultData();
		Assert(ht.Count==3);
		
		bool thrown=false;
		try {
			ht.Add("k2","cool");
		} catch (ArgumentException) {thrown=true;}
		Assert("Must throw ArgumentException!",thrown);
		
		ht["k2"]="cool";
		Assert(ht.Count==3);
		Assert(ht["k2"].Equals("cool"));
		
	}
	
	public void TestCopyTo2() {
		SetDefaultData();
		Object[] entries=new Object[ht.Count];
		ht.CopyTo(entries,0);
		Assert("Not an entry.",entries[0] is DictionaryEntry);
	}
	
	
	public void TestUnderHeavyLoad() {
		ht.Clear();
		int max=100000;
		String[] cache=new String[max*2];
		int n=0;
		
		for (int i=0;i<max;i++) {
			int id=rnd.Next()&0xFFFF;
			String key=""+id+"-key-"+id;
			String val="value-"+id;
			if (ht[key]==null) {
				ht[key]=val;
				cache[n]=key;
				cache[n+max]=val;
				n++;
			}
		}
		
		Assert(ht.Count==n);
		
		for (int i=0;i<n;i++) {
			String key=cache[i];
			String val=ht[key] as String;
			String err="ht[\""+key+"\"]=\""+val+
				"\", expected \""+cache[i+max]+"\"";
			Assert(err,val!=null && val.Equals(cache[i+max]));
		}
		
		int r1=(n/3);
		int r2=r1+(n/5);
		
		for (int i=r1;i<r2;i++) {
			ht.Remove(cache[i]);
		}
		
		
		for (int i=0;i<n;i++) {
			if (i>=r1 && i<r2) {
				Assert(ht[cache[i]]==null);
			} else {
				String key=cache[i];
				String val=ht[key] as String;
				String err="ht[\""+key+"\"]=\""+val+
					"\", expected \""+cache[i+max]+"\"";
				Assert(err,val!=null && val.Equals(cache[i+max]));
			}
		}
		
		ICollection keys=ht.Keys;
		int nKeys=0;
		foreach (Object key in keys) {
			Assert((key as String) != null);
			nKeys++;
		}
		Assert(nKeys==ht.Count);

		
		ICollection vals=ht.Values;
		int nVals=0;
		foreach (Object val in vals) {
			Assert((val as String) != null);
			nVals++;
		}
		Assert(nVals==ht.Count);
		
	}

	
	private class  HashtableTest2 : TestCase {

		protected Hashtable ht;
		private static Random rnd;
		
		public HashtableTest2 (String name) : base(name)
		{
		}
		
		protected override void SetUp ()
		{
			ht=new Hashtable ();
			rnd=new Random ();
		}
		
		public static ITest Suite
		{
			get {
				return new TestSuite (typeof(HashtableTest2));
			}
		}
		
		private void SetDefaultData ()
		{
			ht.Clear ();
			ht.Add ("k1","another");
			ht.Add ("k2","yet");
			ht.Add ("k3","hashtable");
		}
		

		public void TestAddRemoveClear ()
		{
			ht.Clear ();
			Assert (ht.Count == 0);
			
			SetDefaultData ();
			Assert (ht.Count == 3);

			bool thrown=false;
			try {
				ht.Add ("k2","cool");
			} catch (ArgumentException) {thrown=true;}
			Assert("Must throw ArgumentException!",thrown);

			ht["k2"]="cool";
			Assert(ht.Count == 3);
			Assert(ht["k2"].Equals("cool"));

		}

		public void TestCopyTo ()
		{
			SetDefaultData ();
			Object[] entries=new Object[ht.Count];
			ht.CopyTo (entries,0);
			Assert("Not an entry.",entries[0] is DictionaryEntry);
		}


		public void TestUnderHeavyLoad ()
		{
			ht.Clear ();

			int max=100000;
			String[] cache=new String[max*2];
			int n=0;

			for (int i=0;i<max;i++) {
				int id=rnd.Next()&0xFFFF;
				String key=""+id+"-key-"+id;
				String val="value-"+id;
				if (ht[key]==null) {
					ht[key]=val;
					cache[n]=key;
					cache[n+max]=val;
					n++;
				}
			}

			Assert(ht.Count==n);

			for (int i=0;i<n;i++) {
				String key=cache[i];
				String val=ht[key] as String;
				String err="ht[\""+key+"\"]=\""+val+
				      "\", expected \""+cache[i+max]+"\"";
				Assert(err,val!=null && val.Equals(cache[i+max]));
			}

			int r1=(n/3);
			int r2=r1+(n/5);

			for (int i=r1;i<r2;i++) {
				ht.Remove(cache[i]);
			}


			for (int i=0;i<n;i++) {
				if (i>=r1 && i<r2) {
					Assert(ht[cache[i]]==null);
				} else {
					String key=cache[i];
					String val=ht[key] as String;
					String err="ht[\""+key+"\"]=\""+val+
					      "\", expected \""+cache[i+max]+"\"";
					Assert(err,val!=null && val.Equals(cache[i+max]));
				}
			}

			ICollection keys=ht.Keys;
			int nKeys=0;
			foreach (Object key in keys) {
				Assert((key as String) != null);
				nKeys++;
			}
			Assert(nKeys==ht.Count);


			ICollection vals=ht.Values;
			int nVals=0;
			foreach (Object val in vals) {
				Assert((val as String) != null);
				nVals++;
			}
			Assert(nVals==ht.Count);
			
		}
		
		/// <summary>
		///  Test hashtable with CaseInsensitiveHashCodeProvider
		///  and CaseInsensitive comparer.
		/// </summary>
		public void TestCaseInsensitive ()
		{
			// Not very meaningfull test, just to make
			// sure that hcp is set properly set.
			Hashtable ciHashtable = new Hashtable(11,1.0f,CaseInsensitiveHashCodeProvider.Default,CaseInsensitiveComparer.Default);
			ciHashtable ["key1"] = "value";
			ciHashtable ["key2"] = "VALUE";
			Assert(ciHashtable ["key1"].Equals ("value"));
			Assert(ciHashtable ["key2"].Equals ("VALUE"));

			ciHashtable ["KEY1"] = "new_value";
			Assert(ciHashtable ["key1"].Equals ("new_value"));

		}


		public void TestCopyConstructor ()
		{
			SetDefaultData ();

			Hashtable htCopy = new Hashtable (ht);

			Assert(ht.Count == htCopy.Count);
		}


		public void TestEnumerator ()
		{
			SetDefaultData ();

			IEnumerator e = ht.GetEnumerator ();

			while (e.MoveNext ()) {}

			Assert (!e.MoveNext ());

		}

		
	}
	
	
}
}
