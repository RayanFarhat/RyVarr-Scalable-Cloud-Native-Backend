<script lang="ts">
    let terms: Term[];
    type Term = {
        title: string;
        paragraph: string;
    };
    terms = [
        {
            title: "Licensing",
            paragraph:
                "Subscription are valid from the date of purchase until the end of time A valid and active License is required in order to access updates and downloads for purchased product, We are referring to the license as the Pro subscription.",
        },
        {
            title: "Transferring Licenses",
            paragraph: "Licenses are non-transferable",
        },
        {
            title: "License Use and Abuse",
            paragraph:
                "We reserve the right to limit or revoke licenses at any time in cases of abuse, such as re-selling access to your account or distributing your account on the open internet. Licenses that are revoked are not eligible for refunds.",
        },
        {
            title: "Software Updates",
            paragraph:
                "Updates are included for free for as long as your account is active.",
        },
        {
            title: "Agreement for Unexpected Events and Product Use",
            paragraph:
                "In the event of a server crash, database loss, or any unforeseen accidents, you agree that if any of these situations prevent you from using our product, you will not pursue legal action against us under any circumstances.",
        },
        {
            title: "Support Services",
            paragraph:
                "We aim to reply to all support requests. We are not expected to address every request; instead, we prioritize the most frequent and crucial ones, handling them as promptly as we can.",
        },
        {
            title: "Refunds",
            paragraph:
                "We acknowledge that our product might not align with everyone's requirements. That's why we provide guided tours before purchase, ensuring clarity about its suitability for you. As our subscription can be to one month, refunds aren't offered due to the short duration and low price. We will only consider issuing a refund if we have ceased the website and server, discontinued the product, or paused it for several days.",
        },
    ];
</script>

<br />
<br />
<br />
<br />
<div class="px-12">
    <h1 class="text-5xl lg:text-7xl">Terms of use & Agreements</h1>
    <br />
    <br />
    <br />
    {#each terms as term}
        <h2 class="text-3xl">{term.title}</h2>
        <br />
        <p class="text-lg">{term.paragraph}</p>
        <br />
        <br />
    {/each}
</div>
