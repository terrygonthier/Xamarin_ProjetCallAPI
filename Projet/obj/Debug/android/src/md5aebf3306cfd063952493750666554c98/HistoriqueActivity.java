package md5aebf3306cfd063952493750666554c98;


public class HistoriqueActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Projet.HistoriqueActivity, Projet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HistoriqueActivity.class, __md_methods);
	}


	public HistoriqueActivity ()
	{
		super ();
		if (getClass () == HistoriqueActivity.class)
			mono.android.TypeManager.Activate ("Projet.HistoriqueActivity, Projet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
