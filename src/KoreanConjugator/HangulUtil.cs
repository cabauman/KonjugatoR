namespace KoreanConjugator;

/// <summary>
/// Represents a utility for performing common operations on Korean letters and syllables.
/// </summary>
public static class HangulUtil
{
    private const int FirstKoreanSyllableCharacterCode = 44032;
    private const int LastKoreanSyllableCharacterCode = 55203;

    private const int NumberOfInitials = 19;
    private const int NumberOfMedials = 21;
    private const int NumberOfFinals = 28;

    private const int FirstModernCompatibilityLetterCharacterCode = 12593;
    private const int LastModernCompatibilityLetterCharacterCode = 12643;

    private const int FirstModernCompatibilityConsonantCharacterCode = 12593;
    private const int LastModernCompatibilityConsonantCharacterCode = 12622;

    private const int FirstModernCompatibilityVowelCharacterCode = 12623;
    private const int LastModernCompatibilityVowelCharacterCode = 12643;

    private const int FirstKoreanLetterCharacterCode = 4352;
    private const int LastKoreanLetterCharacterCode = 4607;

    private const int FirstModernInitialCharacterCode = 4352;
    private const int LastModernInitialCharacterCode = 4370;

    private const int FirstModernMedialCharacterCode = 4449;
    private const int LastModernMedialCharacterCode = 4469;

    private const int FirstModernFinalCharacterCode = 4520;
    private const int LastModernFinalCharacterCode = 4546;

    private static readonly string CompatibilityInitials = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
    private static readonly string Initials = "ᄀᄁᄂᄃᄄᄅᄆᄇᄈᄉᄊᄋᄌᄍᄎᄏᄐᄑᄒ";
    private static readonly string CompatibilityMedials = "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ";
    private static readonly string Medials = "ᅡᅢᅣᅤᅥᅦᅧᅨᅩᅪᅫᅬᅭᅮᅯᅰᅱᅲᅳᅴᅵ";
    private static readonly string CompatibilityFinals = "\0ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ";
    private static readonly string Finals = "\0ᆨᆩᆪᆫᆬᆭᆮᆯᆰᆱᆲᆳᆴᆵᆶᆷᆸᆹᆺᆻᆼᆽᆾᆿᇀᇁᇂ";

    private static readonly string ModernCompatibilityLetters = CompatibilityInitials + CompatibilityMedials + CompatibilityFinals;
    private static readonly string ModernComposableLetters = Initials + Medials + Finals;

    private static readonly Dictionary<char, char> CompatibilityToComposableFinalMap = CompatibilityFinals
        .Zip(Finals, (k, v) => new { k, v })
        .ToDictionary(x => x.k, x => x.v);

    private static readonly char[] Irregulars = { 'ᆺ', 'ᆮ', 'ᆸ', 'ᅳ', '르', 'ᆯ', 'ᇂ' };

    private static readonly Dictionary<(char, char), char> VowelContractionMap = new()
    {
        { ('ᅡ', 'ᅡ'), 'ᅡ' },
        { ('ᅥ', 'ᅥ'), 'ᅥ' },
        { ('ᅢ', 'ᅥ'), 'ᅢ' },
        { ('ᅢ', 'ᅡ'), 'ᅢ' },
        { ('ᅡ', 'ᅵ'), 'ᅢ' },
        { ('ᅥ', 'ᅵ'), 'ᅢ' },
        { ('ᅣ', 'ᅵ'), 'ᅤ' },
        { ('ᅤ', 'ᅥ'), 'ᅤ' },
        { ('ᅤ', 'ᅡ'), 'ᅤ' },
        { ('ᅦ', 'ᅥ'), 'ᅦ' },
        { ('ᅩ', 'ᅡ'), 'ᅪ' },
        { ('ᅮ', 'ᅥ'), 'ᅯ' },
        { ('ᅮ', 'ᅳ'), 'ᅮ' },
        { ('ᅬ', 'ᅥ'), 'ᅫ' },
        { ('ᅳ', 'ᅡ'), 'ᅡ' },
        { ('ᅳ', 'ᅥ'), 'ᅥ' },
        { ('ᅡ', 'ᅳ'), 'ᅡ' },
        { ('ᅥ', 'ᅳ'), 'ᅥ' },
        { ('ᅣ', 'ᅳ'), 'ᅣ' },
        { ('ᅵ', 'ᅥ'), 'ᅧ' },
        { ('ᅡ', 'ᅧ'), 'ᅢ' },
    };

    private static readonly Dictionary<string, string> SpecialHonorificMap = new()
    {
        { "먹", "들" },        // 잡수시다; 드시다
        { "있", "계" },        // 계시다
        { "자", "주무" },      // 주무시다
        { "말하", "말씀하" },  // 말씀하시다
        { "주", "드리" },      // Not 드리시다; when the receiver deserves hight respect
        { "에게", "께" },
        { "한테", "께" },
        { "말", "말씀" },
        { "보", "뵈" },       // or slightly even more formal 뵙.
    };

    /// <summary>
    /// Gets the regular 이다 verbs.
    /// </summary>
    public static HashSet<string> RegularIdaVerbs { get; } =
    [
        "가려보이","가로놓이","가로누이","가로채이","가리이","간종이",
        "갈라붙이","갈마들이","갈붙이","갈아들이","갈아붙이","감싸이",
        "갸울이","거두어들이","거둬들이","거머들이","건너다보이",
        "건중이","걷어붙이","걸머메이","걸메이","걸채이","걸터들이",
        "겉뜨이","겉절이","겹쌓이","곁들이","곁붙이","고이","고이",
        "곤두박이","곱꺾이","곱놓이","곱들이","곱먹이","공들이",
        "괴이","괴이","굽어보이","굽죄이","그러들이","기울이",
        "기이","기죽이","길들이","깃들이","깊이","까붙이","까이",
        "깎이","깎이","깔보이","깨이","꺄울이","꺼들이","꺾이",
        "꼬이","꼬이","꼬이","꼬이","꼽들이","꾀이","꾸이",
        "꾸이","꿰이","끄숙이","끄집어들이","끊이","끌어들이",
        "끓이","끼울이","끼이","끼이","나누이","나덤벙이",
        "나번득이","낚이","낮보이","내놓이","내다보이","내려다보이",
        "내려붙이","내리덮이","내리먹이","내리쌓이","내리쪼이",
        "내보이","내보이","내붙이","넘겨다보이","넘보이","노느이",
        "녹이","높이","놓아먹이","놓이","누이","누이","누이",
        "누이","누이","눅이","눈기이","늘이","늘이","늘줄이",
        "다가붙이","다붙이","닦이","닦이","달이","답쌓이",
        "덧끼이","덧놓이","덧덮이","덧들이","덧들이","덧보이",
        "덧붙이","덧쌓이","덮싸이","덮쌓이","덮이","데겅이",
        "도두보이","돋보이","돌려다붙이","동이","되박이","되쓰이",
        "되치이","둘러싸이","뒤꼬이","뒤놓이","뒤덮이","뒤바꾸이",
        "뒤방이","뒤볶이","뒤섞이","뒤엎이","드높이","드러쌓이",
        "드러장이","들뜨이","들볶이","들여다보이","들이끼이",
        "들이끼이","들이","들이","들이쌓이","들이쌓이","땋이",
        "때려누이","때려죽이","떠먹이","떠벌이","떠이","떼이",
        "뜨이","뜨이","뜨이","뜨이","뜯어벌이","맞놓이",
        "맞바라보이","맞붙이","맞아들이","맞쪼이","맡이",
        "매손붙이","매이","매이","매이","매조이","먹이",
        "메다붙이","메붙이","메어붙이","메이","명씨박이",
        "모아들이","모이","몰아들이","몰아붙이","무이","묶이",
        "물들이","물어들이","미이","밀어붙이","밀치이","밉보이",
        "바꾸이","바라보이","박이","박이","받아들이","발보이",
        "발붙이","방이","밭이","밭이","배꼬이","배붙이",
        "배붙이","번갈아들이","벋놓이","벌어들이","벌이","베이",
        "보이","보이","보쟁이","보채이","볶이","부딪치이",
        "부레끓이","부리이","부치이","불러들이","불붙이","붙동이",
        "붙매이","붙박이","붙이","비꼬이","비뚤이","비추이",
        "빗놓이","빗보이","빨아들이","뻗장이","삐뚤이","사들이",
        "사이","삭이","삭이","생청붙이","석이","섞이","선들이",
        "선보이","설죽이","속이","숙이","숨죽이","쉬이","싸이",
        "싸이","쌓이","썩이","쏘아붙이","쏘이","쏘이","쏴붙이",
        "쓰이","쓰이","쓰이","쓰이","알아방이","애먹이",
        "야코죽이","얕보이","어녹이","얼녹이","얼보이","얽동이",
        "얽매이","얽섞이","엇깎이","엇누이","엇바꾸이","엇베이",
        "엇붙이","엇섞이","엎이","에워싸이","에이","엮이",
        "열어붙이","엿보이","옥이","옥조이","옥죄이","올려붙이",
        "옭매이","옴츠러들이","욕보이","우러러보이","우줅이","욱이",
        "욱조이","욱죄이","움츠러들이","윽죄이","익삭이",
        "잡아들이","장가들이","쟁이","절이","접붙이","접치이",
        "정들이","정붙이","조이","졸이","죄이","죽이","죽이",
        "줄이","쥐이","쥐이","지이","짓볶이","짓죽이","짜이",
        "쪼이","쪼이","쪼이","쬐이","차이","처들이","처먹이",
        "처쟁이","쳐다보이","추이","축이","충이","치먹이",
        "치쌓이","치이","치이","치이","치이","치이","치이",
        "치이","켜이","트이","파이","펴이","풀어먹이","할퀴이",
        "핥이","핥이","헛놓이","헛보이","헛짚이","헝클이",
        "홀라들이","홅이","훌닦이","훌라들이","훑이","휘덮이",
        "휘어붙이","휩싸이","흘레붙이","흙들이","흩이","힘들이",
        "모이"
    ];

    /// <summary>
    /// Gets words that look irregular but actually aren't.
    /// </summary>
    public static HashSet<string> IrregularExceptions { get; } =
    [
        "털썩이잡","넘겨잡","우접","입","맞접","문잡","다잡","까뒤집","배좁","목잡","끄집","잡","옴켜잡","검잡","되순라잡","내씹","모집","따잡","엇잡","까집","겹집","줄통뽑","버르집","지르잡","추켜잡","업","되술래잡","되접","좁디좁","더위잡","말씹","내뽑","집","걸머잡","휘어잡","꿰입","황잡","에굽","내굽","따라잡","맞뒤집","둘러업","늘잡","끄잡","우그려잡","어줍"," 언걸입","들이곱","껴잡","곱접","훔켜잡","늦추잡","갈아입","친좁","희짜뽑","마음잡","개미잡","옴씹","치잡","그러잡","움켜잡","씹","비집","꼽","살잡","죄입","졸잡","가려잡","뽑","걷어잡","헐잡","돌라입","덧잡","얕잡","낫잡","부여잡","맞붙잡","걸입","주름잡","걷어입","빌미잡","개잡","겉잡","안쫑잡","좁","힘입","걷잡","바르집","감씹","짓씹","손잡","포집","붙잡","낮잡","책잡","곱잡","흉잡","뒤집","땡잡","어림잡","덧껴입","수줍","뒤잡","꼬집","예굽","덮쳐잡","헛잡","되씹","낮추잡","날파람잡","틀어잡","헤집","남의달잡","바로잡","흠잡","파잡","얼추잡","손꼽","접","차려입","골라잡","거머잡","후려잡"," 머줍","넉장뽑","사로잡","덧입","껴입","얼입","우집","설잡","늦잡","비좁","고르잡","때려잡","떼집","되잡","홈켜잡","내곱","곱씹","빼입","들이굽","새잡","이르집","떨쳐입",
        "내솟","빗","드솟","비웃","뺏","샘솟","벗","들이웃","솟","되뺏","빼앗","밧","애긋","짜드라웃","어그솟","들솟","씻","빨가벗","깃","벌거벗","엇","되빼앗","웃","앗","헐벗","용솟","덧솟","발가벗","뻘거벗","날솟","치솟",
        "맞받","내딛","내리받","벋","뒤닫","주고받","공얻","무뜯","물어뜯","여닫","그러묻","잇닫","덧묻","되받","뻗","올리닫"," 헐뜯","들이닫","활걷","겉묻","닫","창받","건네받","물손받","들이받","강요받","내리벋","받","이어받","부르걷","응받","검 뜯","인정받","내려딛","내쏟","내리뻗","너름받","세받","내돋","돌려받","쥐어뜯","껴묻","본받","뒤받","강종받","내리닫"," 떠받","테받","내받","흠뜯","두남받","치받","부르돋","대받","설굳","처닫","얻","들이돋","돋","죄받","쏟","씨받","딱장받","치걷","믿","치벋","버림받","북돋","딛","치고받","욱걷","물려받","뜯","줴뜯","넘겨받","안받","내뻗","내리쏟","벋딛","뒤 묻","뻗딛","치뻗","치닫","줄밑걷","굳","내닫","내림받",
        "들이좋","터놓","접어놓","좋","풀어놓","내쌓","꼴좋","치쌓","물어넣","잇닿","끝닿","그러넣","뽕놓","낳","내리찧","힘닿","내려놓","세놓","둘러놓","들놓","맞찧","잡아넣","돌라쌓","덧쌓","갈라땋","주놓","갈라놓","들이닿","집어넣","닿","의좋","막놓","내놓","들여놓","사놓","썰레놓","짓찧","벋놓","찧","침놓","들이찧","둘러쌓","털어놓","담쌓","돌라놓","되잡아넣"," 끌어넣","덧놓","맞닿","처넣","빻","뻥놓","내리쌓","곱놓","설레발놓","우겨넣","놓","수놓","써넣","널어놓","덮쌓","연닿","헛놓","돌려놓","되쌓","욱여넣","앗아넣","올려놓","헛방놓","날아놓","뒤놓","업수놓","가로놓","맞놓","펴놓","내켜놓","쌓","끙짜놓","들이쌓","겹쌓","기추놓","넣","불어넣","늘어놓","긁어놓","어긋놓","앞넣","눌러놓","땋","들여쌓","빗놓","사이좋","되놓","헛불놓","몰아넣","먹놓","밀쳐놓","살닿","피새놓","빼놓","하차놓","틀어넣",
        "우러르","따르","붙따르","늦치르","다다르","잇따르","치르"
    ];

    /// <summary>
    /// Gets words that are both regular and irregular.
    /// </summary>
    public static HashSet<string> BothRegularAndIrregular { get; } =
    [
        "일","곱","파묻","누르","묻","이르","되묻","썰","붓","들까불","굽","걷","뒤까불","까불"
    ];

    /// <summary>
    /// Gets the index of the Korean letter in the "Initial" position.
    /// </summary>
    /// <param name="syllable">The Korean syllable.</param>
    /// <returns>The index of the Korean letter in the "Initial" position.</returns>
    public static int IndexOfInitial(char syllable)
    {
        return (syllable - FirstKoreanSyllableCharacterCode) / (NumberOfMedials * NumberOfFinals);
    }

    /// <summary>
    /// Gets the Korean letter in the "Initial" position.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The Korean letter in the "Initial" position.</returns>
    public static char Initial(char syllable)
    {
        var initialOffset = IndexOfInitial(syllable);

        return (char)(FirstModernInitialCharacterCode + initialOffset);
    }

    /// <summary>
    /// Gets the index of the Korean letter in the "Medial" position.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The index of the Korean letter in the "Medial" position.</returns>
    public static int IndexOfMedial(char syllable)
    {
        return ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) / NumberOfFinals;
    }

    /// <summary>
    /// Gets the Korean letter in the "Medial" position.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The Korean letter in the "Medial" position.</returns>
    public static char Medial(char syllable)
    {
        var medialOffset = IndexOfMedial(syllable);

        return (char)(FirstModernMedialCharacterCode + medialOffset);
    }

    /// <summary>
    /// Gets the index of the Korean letter in the "Final" position.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The index of the Korean letter in the "Final" position.</returns>
    public static int IndexOfFinal(char syllable)
    {
        return (syllable - FirstKoreanSyllableCharacterCode) % NumberOfFinals;
    }

    /// <summary>
    /// Gets the Korean letter in the "Final" position.
    /// </summary>
    /// <remarks>
    /// Arithmetic implementation: (char)(FirstModernFinalCharacterCode + finalOffset - 1);
    /// The arithmetic implementation isn't used here because of the '\0' special case.
    /// </remarks>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The Korean letter in the "Final" position.</returns>
    public static char Final(char syllable)
    {
        var finalOffset = IndexOfFinal(syllable);

        return Finals[finalOffset];
    }

    /// <summary>
    /// Gets a value indicating whether the syllable contains a Korean letter in the "Final" position.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns><c>true</c> if the syllable has a Final; otherwise, <c>false</c>.</returns>
    public static bool HasFinal(char syllable)
    {
        var finalOffset = ((syllable - FirstKoreanSyllableCharacterCode) % (NumberOfMedials * NumberOfFinals)) % NumberOfFinals;

        return finalOffset > 0;
    }

    /// <summary>
    /// Gets a value indicating whether the syllable begins with a vowel.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns><c>true</c> if the syllable begins with a vowel; otherwise, <c>false</c>.</returns>
    public static bool BeginsWithVowel(char syllable)
    {
        return Initial(syllable) == 'ᄋ';
    }

    /// <summary>
    /// Removes the Korean letter in the "Final" position of the syllable.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns>The resulting Korean syllable.</returns>
    public static char DropFinal(char syllable)
    {
        return SetFinal(syllable, '\0');
    }

    /// <summary>
    /// Sets the Korean letter in the "Final" position of the syllable.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <param name="final">The Korean letter to place in the "Final" position.</param>
    /// <returns>The resulting Korean syllable.</returns>
    public static char SetFinal(char syllable, char final)
    {
        var initial = Initial(syllable);
        var medial = Medial(syllable);
        var newSyllable = Construct(initial, medial, final);
        return newSyllable;
    }

    /// <summary>
    /// Gets a value indicating whether the syllable begins with 'ㄴ'.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns><c>true</c> if the syllable begins with 'ㄴ'; otherwise, <c>false</c>.</returns>
    public static bool BeginsWithNieun(char syllable)
    {
        return Initial(syllable) == 'ᄂ';
    }

    /// <summary>
    /// Gets a value indicating whether the syllable begins with 'ㅅ'.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <returns><c>true</c> if the syllable begins with 'ㅅ'; otherwise, <c>false</c>.</returns>
    public static bool BeginsWithSieut(char syllable)
    {
        return Initial(syllable) == 'ᄉ';
    }

    /// <summary>
    /// Gets a value indicating whether the syllable begins with 'ㅅ'.
    /// </summary>
    /// <param name="syllable">A Korean syllable.</param>
    /// <param name="initial">A Korean letter that belongs in the "Initial" position of a syllable.</param>
    /// <param name="medial">A Korean letter that belongs in the "Medial" position of a syllable.</param>
    /// <returns><c>true</c> if the syllable begins with 'ㅅ'; otherwise, <c>false</c>.</returns>
    public static bool BeginsWith(char syllable, char initial, char medial)
    {
        return IsSyllable(syllable) && Initial(syllable) == initial && Medial(syllable) == medial;
    }

    /// <summary>
    /// Constructs a Korean syllable.
    /// </summary>
    /// <param name="initial">The index of the initial jamo character.</param>
    /// <param name="medial">The index of the medial jamo character.</param>
    /// <param name="final">The index of the final jamo character.</param>
    /// <returns>The constructed Korean syllable.</returns>
    public static char Construct(int initial, int medial, int final)
    {
        if (initial < 0 || initial >= NumberOfInitials)
        {
            throw new ArgumentOutOfRangeException(nameof(initial), $"The index of the initial character must be between 0 and {NumberOfInitials - 1}");
        }

        if (medial < 0 || medial >= NumberOfMedials)
        {
            throw new ArgumentOutOfRangeException(nameof(medial), $"The index of the medial character must be between 0 and {NumberOfMedials - 1}");
        }

        if (final < 0 || final >= NumberOfFinals)
        {
            throw new ArgumentOutOfRangeException(nameof(final), $"The index of the final character must be between 0 and {NumberOfFinals - 1}");
        }

        int characterCode = (initial * NumberOfMedials * NumberOfFinals) + (medial * NumberOfFinals) + final + FirstKoreanSyllableCharacterCode;

        return (char)characterCode;
    }

    /// <summary>
    /// Constructs a syllable from individual letters.
    /// </summary>
    /// <param name="initial">The initial.</param>
    /// <param name="medial">The medial.</param>
    /// <param name="final">The final.</param>
    /// <returns>A constructed syllable.</returns>
    public static char Construct(char initial, char medial, char final)
    {
        int indexOfFinal = final.Equals('\0') ? 0 : final - FirstModernFinalCharacterCode + 1;

        return Construct(
            initial - FirstModernInitialCharacterCode,
            medial - FirstModernMedialCharacterCode,
            indexOfFinal);
    }

    /// <summary>
    /// Gets a value indicating whether the two characters can be contracted into a single character.
    /// </summary>
    /// <param name="character1">Character 1.</param>
    /// <param name="character2">Character 2.</param>
    /// <returns><c>true</c> if the characters can be contracted; otherwise, <c>false</c>.</returns>
    public static bool CanContract(char character1, char character2)
    {
        var medial1 = Medial(character1);
        var medial2 = Medial(character2);
        var key1 = (medial1, medial2);
        var key2 = (character1, medial2);

        return !IsModernCompatibilityLetter(character1) &&
            KoreanLetter.Ieung.Equals(Initial(character2)) &&
            (VowelContractionMap.ContainsKey(key1) || VowelContractionMap.ContainsKey(key2));
    }

    /// <summary>
    /// Contracts two syllables into one.
    /// </summary>
    /// <param name="syllable1">The first syllable.</param>
    /// <param name="syllable2">The second syllable.</param>
    /// <returns>The contracted Korean syllable.</returns>
    public static char Contract(char syllable1, char syllable2)
    {
        var medial1 = Medial(syllable1);
        var medial2 = Medial(syllable2);
        if (!VowelContractionMap.TryGetValue((medial1, medial2), out char medial))
        {
            throw new Exception($"Tried to contract {syllable1} and {syllable2} but" +
                "couldn't find a valid contraction.");
        }

        return Construct(Initial(syllable1), medial, Final(syllable2));
    }

    /// <summary>
    /// Gets a value indicating whether the verb stem is irregular.
    /// </summary>
    /// <param name="verbStem">A verb without '다' at the end.</param>
    /// <returns><c>true</c> if the verb stem is irregular; otherwise, <c>false</c>.</returns>
    public static bool IsIrregular(string verbStem)
    {
        ArgumentException.ThrowIfNullOrEmpty(verbStem);

        bool hasIrregularForm =
            Irregulars.Contains(Final(verbStem[^1])) ||
            Irregulars.Contains(Medial(verbStem[^1])) ||
            Irregulars.Contains(verbStem[^1]);

        return
            IsSyllable(verbStem[^1]) &&
            hasIrregularForm &&
            !IrregularExceptions.Contains(verbStem);
    }

    /// <summary>
    /// Gets a value indicating whether the character is a composable letter.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns><c>true</c> if the character is a composable letter; otherwise, <c>false</c>.</returns>
    public static bool IsComposableLetter(char character)
    {
        return character >= FirstKoreanLetterCharacterCode && character <= LastKoreanLetterCharacterCode;
    }

    /// <summary>
    /// Gets a value indicating whether the character is a modern compatibility letter.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns><c>true</c> if the character is a modern compatibility letter; otherwise, <c>false</c>.</returns>
    public static bool IsModernCompatibilityLetter(char character)
    {
        return character >= FirstModernCompatibilityLetterCharacterCode && character <= LastModernCompatibilityLetterCharacterCode;
    }

    /// <summary>
    /// Gets a value indicating whether the character is a Korean syllable.
    /// </summary>
    /// <param name="character">The character to check.</param>
    /// <returns><c>true</c> if the character is a Korean syllable; otherwise, <c>false</c>.</returns>
    public static bool IsSyllable(char character)
    {
        return character >= FirstKoreanSyllableCharacterCode && character <= LastKoreanSyllableCharacterCode;
    }

    /// <summary>
    /// Gets the composable version of a compatibility letter.
    /// </summary>
    /// <param name="compatibilityLetter">A compatibility letter.</param>
    /// <returns>A composable Korean letter that belongs in the "Final" position of a syllable.</returns>
    public static char ToComposableFinal(char compatibilityLetter)
    {
        if (!CompatibilityToComposableFinalMap.TryGetValue(compatibilityLetter, out var result))
        {
            throw new ArgumentException($"[{compatibilityLetter}] is not a compatibility letter.");
        }

        return result;
    }

    /// <summary>
    /// Gets the index of the "Final".
    /// </summary>
    /// <param name="final">A Korean letter that belongs in the "Final" position of a syllable.</param>
    /// <returns>The index of the "Final".</returns>
    public static int FinalToIndex(char final)
    {
        return (final + 1) - FirstModernFinalCharacterCode;
    }

    /// <summary>
    /// Gets the honorific form of the verb stem, if one exists.
    /// </summary>
    /// <param name="verbStem">The verb stem.</param>
    /// <returns>The honorific form of he verb stem, if one exists; otherwise, <c>null</c>.</returns>
    public static string? GetSpecialHonorificForm(string verbStem)
    {
        SpecialHonorificMap.TryGetValue(verbStem, out var honorificStem);
        return honorificStem;
    }
}
