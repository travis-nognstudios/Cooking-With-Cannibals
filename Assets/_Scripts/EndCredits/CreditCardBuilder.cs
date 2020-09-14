using UnityEngine;
using UnityEngine.UI;

namespace EndCredits
{
    public class CreditCardBuilder : MonoBehaviour
    {
        public RawImage profilePicture;
        public Text personName;

        public CreditRoleBuilder role1;
        public CreditRoleBuilder role2;
        public CreditRoleBuilder role3;

        private CreditDetails details;

        private void Start()
        {
            details = GetComponent<CreditDetails>();
            BuildCard();
        }

        private void BuildCard()
        {
            SetProfilePicture(details.profilePicture);
            SetName(details.personName);

            int numRoles = details.roles.Length;
            for (int i=0; i<numRoles; ++i)
            {
                string amount = details.roles[i].amount;
                string role = details.roles[i].role;
                SetRole(amount, role, i);
            }
        }

        void SetProfilePicture(Texture pic)
        {
            profilePicture.texture = pic;
        }

        void SetName(string name)
        {
            personName.text = name;
        }

        void SetRole(string amount, string role, int lineIndex)
        {
            CreditRoleBuilder selectedLine = role1;

            if (lineIndex == 1)
                selectedLine = role2;
            else if (lineIndex == 2)
                selectedLine = role3;
            else
                selectedLine = role1;

            selectedLine.amount.text = amount;
            selectedLine.role.text = role;
        }
    }
}