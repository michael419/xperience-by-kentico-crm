//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.OnlineForms;
using CMS.OnlineForms.Types;

[assembly: RegisterBizForm(DancingGoatContactUsItem.CLASS_NAME, typeof(DancingGoatContactUsItem))]

namespace CMS.OnlineForms.Types
{
	/// <summary>
	/// Represents a content item of type DancingGoatContactUsItem.
	/// </summary>
	public partial class DancingGoatContactUsItem : BizFormItem
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "BizForm.DancingGoatContactUs";


		/// <summary>
		/// The instance of the class that provides extended API for working with DancingGoatContactUsItem fields.
		/// </summary>
		private readonly DancingGoatContactUsItemFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// UserFirstName.
		/// </summary>
		[DatabaseField]
		public string UserFirstName
		{
			get => ValidationHelper.GetString(GetValue(nameof(UserFirstName)), @"");
			set => SetValue(nameof(UserFirstName), value);
		}


		/// <summary>
		/// UserLastName.
		/// </summary>
		[DatabaseField]
		public string UserLastName
		{
			get => ValidationHelper.GetString(GetValue(nameof(UserLastName)), @"");
			set => SetValue(nameof(UserLastName), value);
		}


		/// <summary>
		/// UserEmail.
		/// </summary>
		[DatabaseField]
		public string UserEmail
		{
			get => ValidationHelper.GetString(GetValue(nameof(UserEmail)), @"");
			set => SetValue(nameof(UserEmail), value);
		}


		/// <summary>
		/// UserMessage.
		/// </summary>
		[DatabaseField]
		public string UserMessage
		{
			get => ValidationHelper.GetString(GetValue(nameof(UserMessage)), @"");
			set => SetValue(nameof(UserMessage), value);
		}


		/// <summary>
		/// Gets an object that provides extended API for working with DancingGoatContactUsItem fields.
		/// </summary>
		[RegisterProperty]
		public DancingGoatContactUsItemFields Fields
		{
			get => mFields;
		}


		/// <summary>
		/// Provides extended API for working with DancingGoatContactUsItem fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class DancingGoatContactUsItemFields : AbstractHierarchicalObject<DancingGoatContactUsItemFields>
		{
			/// <summary>
			/// The content item of type DancingGoatContactUsItem that is a target of the extended API.
			/// </summary>
			private readonly DancingGoatContactUsItem mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="DancingGoatContactUsItemFields" /> class with the specified content item of type DancingGoatContactUsItem.
			/// </summary>
			/// <param name="instance">The content item of type DancingGoatContactUsItem that is a target of the extended API.</param>
			public DancingGoatContactUsItemFields(DancingGoatContactUsItem instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// UserFirstName.
			/// </summary>
			public string UserFirstName
			{
				get => mInstance.UserFirstName;
				set => mInstance.UserFirstName = value;
			}


			/// <summary>
			/// UserLastName.
			/// </summary>
			public string UserLastName
			{
				get => mInstance.UserLastName;
				set => mInstance.UserLastName = value;
			}


			/// <summary>
			/// UserEmail.
			/// </summary>
			public string UserEmail
			{
				get => mInstance.UserEmail;
				set => mInstance.UserEmail = value;
			}


			/// <summary>
			/// UserMessage.
			/// </summary>
			public string UserMessage
			{
				get => mInstance.UserMessage;
				set => mInstance.UserMessage = value;
			}
		}

		#endregion


		#region "Constructors"

		/// <summary>
		/// Initializes a new instance of the <see cref="DancingGoatContactUsItem" /> class.
		/// </summary>
		public DancingGoatContactUsItem() : base(CLASS_NAME)
		{
			mFields = new DancingGoatContactUsItemFields(this);
		}

		#endregion
	}
}