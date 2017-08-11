﻿using System.Windows.Controls;

namespace DQ11
{
	abstract class CharStatus
	{
		protected uint Base { get; private set; } = 0;

		public virtual void Init() { }
		public void Load(Control parent)
		{
			if (parent == null) return;
			Base = (uint)parent.Tag;
		}

		public abstract void Read();
		public abstract void Write();
		public virtual void Copy() { }
		public virtual void Paste() { }
		public virtual void Max() { }
		public virtual void Min() { }
	}
}
