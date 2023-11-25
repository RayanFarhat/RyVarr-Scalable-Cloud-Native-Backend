<script>
  import { page } from "$app/stores";
  import { onMount } from "svelte";
  import { getAccountData } from "../../core/accountData";
  let btnUrl = "/login";
  let btnTitle = "Login";

  onMount(() => {
    if (getAccountData().token != "") {
      btnUrl = "/profile";
      btnTitle = "Profile";
    } else {
      btnUrl = "/login";
      btnTitle = "Login";
    }
  });
</script>

<div class="flex-none hidden lg:block">
  <ul class="menu menu-horizontal">
    <!-- Navbar menu content here -->

    <li>
      <a class="p-0 hover:bg-transparent" href="/pricing">
        <button
          class="btn btn-outline border-0 hover:bg-transparent hover:text-primary"
        >
          Pricing</button
        ></a
      >
    </li>

    <li>
      <a class=" mr-12 p-0 hover:bg-transparent" href="/hamzacad">
        <button
          class="btn btn-outline border-0 hover:bg-transparent hover:text-primary"
        >
          Hamzacad</button
        ></a
      >
    </li>

    <li>
      <a class="p-0 hover:bg-transparentt" href={btnUrl}>
        <button class="btn btn-outline btn-primary">{btnTitle}</button></a
      >
    </li>

    <li>
      <a class="p-0 hover:bg-transparentt" href="/">
        <button class="btn btn-primary"
          >{$page.url.pathname.includes("/hamzacad")
            ? "Download"
            : "Let's Talk"}</button
        ></a
      >
    </li>
  </ul>
</div>
