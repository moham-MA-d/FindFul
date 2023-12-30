using System.ComponentModel;

namespace DTO.Enumerations
{
    public class UserEnums
    {
        public enum Sex
        {
            All = 0,
            Female = 1,
            Male = 2,
            RatherNotToSay = 3,
            Other = 4,
        }

        //https://www.womenshealthmag.com/relationships/a36395721/gender-identity-list/
        public enum Gender
        {
            [Description("All")]
            All = 0,

            [Description("In conversations about gender, you may hear this expression used. Gender identity is about one's psychological sense of self. Anatomical sex is comprised of things like genitals, chromosomes, hormones, body hair, and more,” says Sophie Mona Pagès, relationship expert and founder of LVRSNFRNDS, a social network that fosters connections and conversations about relationships. You may be familiar with the terms ‘AFAB’ (Assigned Female At Birth) and ‘AMAB ’(Assigned Male At Birth). This is what they are about.” AFAB and AMAB people may not choose to identify with those terms during childhood, or when they become adolescents or adults.")]
            Anatomical_sex,

            [Description("This term describes a person whose gender identity aligns with or matches their assigned sex at birth. If a doctor assigns gender based on genitalia, when the baby is born and says, ‘It's a girl,’ and that person aligns with their gender, that's what it means to be cisgender,” says Golob, noting that this term sometimes gets shortened to Cis.”")]
            Cisgender,


            [Description("Transgender people are folks whose gender identity does not match their sex assigned at birth. They can be trans men, trans women, and also non-binary people, meaning they do not identify as men or women,” says Pagès. She adds one quick note: Do not say ‘a trans’ or ‘a transgender.’ Say instead, ‘a trans person,’ ‘a transgender person,’ ‘a trans man,’ or ‘a trans woman.’”")]
            Transgender,


            [Description("Cishet refers to people whose gender identity and biological sex are aligned (cisgender), and who are sexually attracted to the opposite sex,” says Marsh. For instance, an individual born with a vagina who identifies as female and is romantically involved with males would be described as Cishet.")]
            Cishet,


            [Description("Non-binary describes a person who does not identify clearly or exclusively as male or a female, says Alexandra Bausic, M.D., a board-certified OBGYN, and sex educator at Let’s Talk Sex. They can either feel both gender characteristics or feel different from them.” You may hear non-binary used as an umbrella term for various groups of people that don’t identify as male or female.")]
            Non_binary,


            [Description("A person born with either some combination of both biological sex characteristics (genital organs, hormones, chromosomes) or certain genital variations that don't align with either biological sex is intersex, explains Bausic. It is a natural variation in human anatomy, and it shouldn’t be perceived as something bad,” she says. Also, it's important to know that being intersex is not that uncommon: Planned Parenthood estimates that one to two people out of every 100 in the U.S. are intersex.")]
            Intersex,


            [Description("Genderqueer people can identify as neither woman nor man, both woman and man, or a combination of these genders,” says Pagès. Sometimes the words genderqueer” and non-binary” are used interchangeably.")]
            Genderqueer,


            [Description("Just like you can be fluid in your sexual orientation of who you’re attracted to, you can also be flexible with your gender. Gender-fluid typically refers to someone who prefers to express either or both maleness or femaleness, and that can vary, perhaps from day to day,” says Marsh.")]
            Gender_fluid,


            [Description("Gender non-conformity refers to when someone does not conform to their cultural gender norms,” says Marsh. It could be something as minute as an Assigned Male At Birth (AMAB) person wearing nail polish, Marsh explains. That could be considered gender non-conforming, since nail polish is typically attributed to female-presenting people in our society. Or, on a larger scale, the person might not choose to identify with he/him or she/her pronouns.")]
            Non_conforming,


            [Description("Agender means that one does not identify with any gender,” says Marsh. They do not feel a sense of male or female,” adds Marsh, noting that like other non-cisgender groups, they may ask to be addressed using the pronouns 'they' or 'them' rather than 'he' or 'she.'")]
            Agender,


            [Description("Gendervoid is a term that is similar to agender, but specifically refers to not only a lack of gender identity, but also a sense of loss or a void in not feeling that gender identity,” explains Marsh. For gendervoid people, they feel like they don’t experience or aren’t able to feel their gender.")]
            Gendervoid,


        }

        public enum OrderBy
        {
            Newest = 0,
            Oldest = 1,
            LastActivity = 2,
            AgeDes = 3,
            AgeAsc = 4,
        }
        public enum LoginInputType
        {
            None = 0,
            Email = 1,
            Phone = 2,
            Username = 3,
        }

        public enum MemberRole
        {
            MemberL1,
            MemberL2
            // Add other roles as needed
        }
        public enum AdminGrant
        {
            AddNewMember,
            DeleteMember,
            AddNewAdmin,
            DeleteAdmin,
            AddNewPost,
            EditAndDeletePost,
            EditAndDeleteOtherAdminsPost,
            ChangeGroupInfo,
        }
    }
}
