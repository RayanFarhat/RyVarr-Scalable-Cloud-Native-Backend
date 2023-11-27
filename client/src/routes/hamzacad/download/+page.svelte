<script lang="ts">
    import { browser } from "$app/environment";
    import {
        getAccountData,
        type AccountData,
    } from "../../../core/accountData";
    import { gotoURL } from "../../../core/gotoURL";

    let userData: AccountData = {
        username: "",
        isPro: false,
        proEndingDate: "",
        token: "",
        expiration: "",
    };
    if (browser) {
        userData = getAccountData();
    }

    async function onDownload() {
        gotoURL("/pricing");
        const endpoint = "http://localhost/api/HamzaCAD/0.0.1";

        const res = await fetch(endpoint, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${userData.token}`,
            },
        });

        let resJson = await res.json();
        // todo this fetch must download installer only if user is pro
    }
</script>

<br />
<br />
<br />
<br />
<div class="grid grid-flow-row lg:grid-cols-3">
    <div class="card shadow-xl bg-base-200 m-4 border border-primary">
        <div class="card-body items-center text-center">
            <h2 class="card-title text-white">HamzaCAD Demo 0.0.1</h2>
            <div class="stat">
                <div
                    class="stat-value text-primary flex items-center justify-center"
                >
                    Autocad 2024
                </div>
                <button on:click={onDownload} class="btn btn-primary mt-8"
                    >Download</button
                >
            </div>
        </div>
    </div>
</div>
