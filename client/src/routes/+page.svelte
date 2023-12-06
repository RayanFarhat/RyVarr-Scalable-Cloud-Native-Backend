<script>
    import { browser } from "$app/environment";
    import Intro from "../components/MainPage/Intro.svelte";
    import Products from "../components/MainPage/Products.svelte";
    import Services from "../components/MainPage/Services.svelte";
    import WhyUs from "../components/MainPage/WhyUs.svelte";
    console.log("aaaaaaaa");
    if (browser) {
        console.log(window.location.origin);
    }
</script>

<Intro />
<Products />
<Services />
<WhyUs />
